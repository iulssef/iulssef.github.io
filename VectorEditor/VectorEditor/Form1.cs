using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VectorEditor.Figures;
using VectorEditor.Drawing;
using VectorEditor.Helpers;
using VectorEditor.History;
using VectorEditor.Serialization;

namespace VectorEditor
{
    public partial class Form1 : Form
    {
        private List<Figure> _figures = new List<Figure>();
        private Figure _selectedFigure;
        private StackMemory _undoStack = new StackMemory(20);
        private Point _startPoint;
        private Point _lastPoint;
        private bool _isDrawing;
        private bool _isMoving;
        private bool _isResizing;
        private SelectionBorder.MarkerType _activeMarker;
        private FigureFactory.FigureType _currentType = FigureFactory.FigureType.Rectangle;

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;

            // Подписываемся на события
            panelCanvas.Paint += PanelCanvas_Paint;
            panelCanvas.MouseDown += PanelCanvas_MouseDown;
            panelCanvas.MouseMove += PanelCanvas_MouseMove;
            panelCanvas.MouseUp += PanelCanvas_MouseUp;
            panelCanvas.KeyDown += PanelCanvas_KeyDown;
            panelCanvas.PreviewKeyDown += PanelCanvas_PreviewKeyDown;

            // Включаем двойную буферизацию
            panelCanvas.GetType().GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(panelCanvas, true);

            SaveState();
            statusLabel.Text = "Готов | Инструмент: Прямоугольник";
        }

        private void PanelCanvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            foreach (var f in _figures)
                f.Draw(e.Graphics);

            if (_selectedFigure != null)
                SelectionBorder.Draw(e.Graphics, _selectedFigure.Bounds);
        }

        private void PanelCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            panelCanvas.Focus();
            _lastPoint = e.Location;

            if (e.Button == MouseButtons.Left)
            {
                // Проверяем маркеры
                if (_selectedFigure != null)
                {
                    _activeMarker = SelectionBorder.GetMarkerAtPoint(_selectedFigure.Bounds, e.Location);
                    if (_activeMarker != SelectionBorder.MarkerType.None)
                    {
                        _isResizing = true;
                        return;
                    }
                }

                // Проверяем попадание в фигуру
                var hit = _figures.LastOrDefault(f => f.HitTest(e.Location));
                if (hit != null)
                {
                    if (_selectedFigure != null) _selectedFigure.IsSelected = false;
                    _selectedFigure = hit;
                    _selectedFigure.IsSelected = true;
                    _isMoving = true;
                    panelCanvas.Invalidate();
                }
                else
                {
                    if (_selectedFigure != null)
                    {
                        _selectedFigure.IsSelected = false;
                        _selectedFigure = null;
                        panelCanvas.Invalidate();
                    }
                    _isDrawing = true;
                    _startPoint = e.Location;
                }
            }
        }

        private void PanelCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDrawing && _startPoint != Point.Empty)
            {
                panelCanvas.Invalidate();
                return;
            }

            if (_isMoving && _selectedFigure != null)
            {
                _selectedFigure.MoveHorizontal(e.X - _lastPoint.X);
                _selectedFigure.MoveVertical(e.Y - _lastPoint.Y);
                _lastPoint = e.Location;
                panelCanvas.Invalidate();
                return;
            }

            if (_isResizing && _selectedFigure != null)
            {
                var newBounds = SelectionBorder.ApplyResize(_selectedFigure.Bounds, _activeMarker,
                    e.X - _lastPoint.X, e.Y - _lastPoint.Y);
                _selectedFigure.Bounds = newBounds;
                _lastPoint = e.Location;
                panelCanvas.Invalidate();
                return;
            }

            statusLabel.Text = $"X: {e.X}, Y: {e.Y} | Фигур: {_figures.Count}";
        }

        private void PanelCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isDrawing && _startPoint != Point.Empty)
            {
                int x = Math.Min(_startPoint.X, e.X);
                int y = Math.Min(_startPoint.Y, e.Y);
                int w = Math.Abs(_startPoint.X - e.X);
                int h = Math.Abs(_startPoint.Y - e.Y);

                if (w > 5 && h > 5)
                {
                    var rect = new Rectangle(x, y, w, h);
                    var newFigure = FigureFactory.CreateFigure(_currentType, rect);
                    _figures.Add(newFigure);

                    if (_selectedFigure != null) _selectedFigure.IsSelected = false;
                    _selectedFigure = newFigure;
                    _selectedFigure.IsSelected = true;
                    panelCanvas.Invalidate();
                    SaveState();
                }
            }

            _isDrawing = false;
            _isMoving = false;
            _isResizing = false;
            _activeMarker = SelectionBorder.MarkerType.None;
            _startPoint = Point.Empty;
            panelCanvas.Invalidate();
        }

        private void PanelCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (_selectedFigure == null) return;

            int step = e.Shift ? 1 : 5;

            if (e.KeyCode == Keys.Left) _selectedFigure.MoveHorizontal(-step);
            else if (e.KeyCode == Keys.Right) _selectedFigure.MoveHorizontal(step);
            else if (e.KeyCode == Keys.Up) _selectedFigure.MoveVertical(-step);
            else if (e.KeyCode == Keys.Down) _selectedFigure.MoveVertical(step);
            else return;

            panelCanvas.Invalidate();
            SaveState();
            e.Handled = true;
        }

        private void PanelCanvas_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right ||
                e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.IsInputKey = true;
        }

        private void SaveState()
        {
            using (var ms = new MemoryStream())
            {
                FigureSerializer.SaveToStream(ms, _figures);
                _undoStack.Push(ms);
            }
            UpdateUndoRedoButtons();
        }

        private void UpdateUndoRedoButtons()
        {
            if (undoMenu != null) undoMenu.Enabled = _undoStack.CanUndo;
            if (redoMenu != null) redoMenu.Enabled = _undoStack.CanRedo;
        }

        private void Undo()
        {
            var ms = _undoStack.Undo();
            if (ms != null)
            {
                _figures = FigureSerializer.LoadFromStream(ms);
                _selectedFigure = null;
                panelCanvas.Invalidate();
                UpdateUndoRedoButtons();
                statusLabel.Text = "Отменено";
            }
        }

        private void Redo()
        {
            var ms = _undoStack.Redo();
            if (ms != null)
            {
                _figures = FigureSerializer.LoadFromStream(ms);
                _selectedFigure = null;
                panelCanvas.Invalidate();
                UpdateUndoRedoButtons();
                statusLabel.Text = "Возвращено";
            }
        }

        private void CopyFigure()
        {
            if (_selectedFigure != null)
            {
                using (var ms = new MemoryStream())
                {
                    var list = new List<Figure> { _selectedFigure };
                    FigureSerializer.SaveToStream(ms, list);
                    Clipboard.SetData("VectorFigure", ms.ToArray());
                }
                statusLabel.Text = "Фигура скопирована";
            }
        }

        private void CutFigure()
        {
            CopyFigure();
            DeleteFigure();
            statusLabel.Text = "Фигура вырезана";
        }

        private void PasteFigure()
        {
            var data = Clipboard.GetData("VectorFigure");
            if (data is byte[] bytes)
            {
                using (var ms = new MemoryStream(bytes))
                {
                    var list = FigureSerializer.LoadFromStream(ms);
                    if (list.Count > 0)
                    {
                        var newFig = list[0].Clone();
                        newFig.MoveHorizontal(20);
                        newFig.MoveVertical(20);
                        _figures.Add(newFig);

                        if (_selectedFigure != null) _selectedFigure.IsSelected = false;
                        _selectedFigure = newFig;
                        _selectedFigure.IsSelected = true;
                        panelCanvas.Invalidate();
                        SaveState();
                        statusLabel.Text = "Фигура вставлена";
                    }
                }
            }
        }

        private void DeleteFigure()
        {
            if (_selectedFigure != null)
            {
                _figures.Remove(_selectedFigure);
                _selectedFigure = null;
                panelCanvas.Invalidate();
                SaveState();
                statusLabel.Text = "Фигура удалена";
            }
        }

        private void NewDocument()
        {
            _figures.Clear();
            _selectedFigure = null;
            _undoStack.Clear();
            panelCanvas.Invalidate();
            SaveState();
            statusLabel.Text = "Новый документ";
        }

        private void OpenDocument()
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Vector files (*.vec)|*.vec";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _figures = FigureSerializer.LoadFromFile(ofd.FileName);
                    _selectedFigure = null;
                    _undoStack.Clear();
                    panelCanvas.Invalidate();
                    SaveState();
                    statusLabel.Text = $"Открыто: {ofd.FileName}";
                }
            }
        }

        private void SaveDocument()
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Vector files (*.vec)|*.vec";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (FigureSerializer.SaveToFile(sfd.FileName, _figures))
                        statusLabel.Text = $"Сохранено: {sfd.FileName}";
                }
            }
        }

        private void SetType(FigureFactory.FigureType type)
        {
            _currentType = type;
            statusLabel.Text = $"Инструмент: {FigureFactory.GetFigureTypeName(type)}";
        }

        private void HoleProperties()
        {
            if (_selectedFigure is HollowRectangleFigure hf)
            {
                var form = new Form();
                form.Text = "Свойства отверстия";
                form.Size = new Size(300, 150);
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;

                var track = new TrackBar
                {
                    Minimum = 10,
                    Maximum = 90,
                    Value = (int)(hf.HolePercent * 100),
                    Dock = DockStyle.Top,
                    Height = 50
                };

                var label = new Label
                {
                    Text = $"Размер отверстия: {hf.HolePercent * 100:F0}%",
                    Dock = DockStyle.Top,
                    Height = 30,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                var btn = new Button
                {
                    Text = "OK",
                    Dock = DockStyle.Bottom,
                    Height = 40,
                    DialogResult = DialogResult.OK
                };

                track.ValueChanged += (s, e) => {
                    label.Text = $"Размер отверстия: {track.Value}%";
                    hf.HolePercent = track.Value / 100f;
                    panelCanvas.Invalidate();
                };

                form.Controls.Add(btn);
                form.Controls.Add(track);
                form.Controls.Add(label);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    SaveState();
                    statusLabel.Text = "Свойства отверстия изменены";
                }
            }
            else
            {
                MessageBox.Show("Выберите фигуру с отверстием", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Обработчики меню
        private void New_Click(object sender, EventArgs e) => NewDocument();
        private void Open_Click(object sender, EventArgs e) => OpenDocument();
        private void Save_Click(object sender, EventArgs e) => SaveDocument();
        private void Exit_Click(object sender, EventArgs e) => Close();
        private void Undo_Click(object sender, EventArgs e) => Undo();
        private void Redo_Click(object sender, EventArgs e) => Redo();
        private void Copy_Click(object sender, EventArgs e) => CopyFigure();
        private void Cut_Click(object sender, EventArgs e) => CutFigure();
        private void Paste_Click(object sender, EventArgs e) => PasteFigure();
        private void Delete_Click(object sender, EventArgs e) => DeleteFigure();
        private void Rect_Click(object sender, EventArgs e) => SetType(FigureFactory.FigureType.Rectangle);
        private void Square_Click(object sender, EventArgs e) => SetType(FigureFactory.FigureType.Square);
        private void HollowRect_Click(object sender, EventArgs e) => SetType(FigureFactory.FigureType.HollowRectangle);
        private void HollowSquare_Click(object sender, EventArgs e) => SetType(FigureFactory.FigureType.HollowSquare);
        private void HoleProps_Click(object sender, EventArgs e) => HoleProperties();
    }
}