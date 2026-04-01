using System.Drawing;
using System.Windows.Forms;

namespace VectorEditor
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // Меню
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileMenu;
        private ToolStripMenuItem newMenu;
        private ToolStripMenuItem openMenu;
        private ToolStripMenuItem saveMenu;
        private ToolStripMenuItem exitMenu;
        private ToolStripMenuItem editMenu;
        private ToolStripMenuItem undoMenu;
        private ToolStripMenuItem redoMenu;
        private ToolStripMenuItem copyMenu;
        private ToolStripMenuItem cutMenu;
        private ToolStripMenuItem pasteMenu;
        private ToolStripMenuItem deleteMenu;
        private ToolStripMenuItem figureMenu;
        private ToolStripMenuItem rectMenu;
        private ToolStripMenuItem squareMenu;
        private ToolStripMenuItem hollowRectMenu;
        private ToolStripMenuItem hollowSquareMenu;
        private ToolStripMenuItem holePropsMenu;

        // Панель инструментов
        private ToolStrip toolStrip;
        private ToolStripButton newBtn;
        private ToolStripButton openBtn;
        private ToolStripButton saveBtn;
        private ToolStripSeparator sep1;
        private ToolStripButton undoBtn;
        private ToolStripButton redoBtn;
        private ToolStripSeparator sep2;
        private ToolStripButton copyBtn;
        private ToolStripButton cutBtn;
        private ToolStripButton pasteBtn;
        private ToolStripSeparator sep3;
        private ToolStripButton rectBtn;
        private ToolStripButton squareBtn;
        private ToolStripButton hollowRectBtn;
        private ToolStripButton hollowSquareBtn;

        // Панель рисования и статус
        private Panel panelCanvas;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStrip = new MenuStrip();
            this.fileMenu = new ToolStripMenuItem();
            this.newMenu = new ToolStripMenuItem();
            this.openMenu = new ToolStripMenuItem();
            this.saveMenu = new ToolStripMenuItem();
            this.exitMenu = new ToolStripMenuItem();
            this.editMenu = new ToolStripMenuItem();
            this.undoMenu = new ToolStripMenuItem();
            this.redoMenu = new ToolStripMenuItem();
            this.copyMenu = new ToolStripMenuItem();
            this.cutMenu = new ToolStripMenuItem();
            this.pasteMenu = new ToolStripMenuItem();
            this.deleteMenu = new ToolStripMenuItem();
            this.figureMenu = new ToolStripMenuItem();
            this.rectMenu = new ToolStripMenuItem();
            this.squareMenu = new ToolStripMenuItem();
            this.hollowRectMenu = new ToolStripMenuItem();
            this.hollowSquareMenu = new ToolStripMenuItem();
            this.holePropsMenu = new ToolStripMenuItem();

            this.toolStrip = new ToolStrip();
            this.newBtn = new ToolStripButton();
            this.openBtn = new ToolStripButton();
            this.saveBtn = new ToolStripButton();
            this.sep1 = new ToolStripSeparator();
            this.undoBtn = new ToolStripButton();
            this.redoBtn = new ToolStripButton();
            this.sep2 = new ToolStripSeparator();
            this.copyBtn = new ToolStripButton();
            this.cutBtn = new ToolStripButton();
            this.pasteBtn = new ToolStripButton();
            this.sep3 = new ToolStripSeparator();
            this.rectBtn = new ToolStripButton();
            this.squareBtn = new ToolStripButton();
            this.hollowRectBtn = new ToolStripButton();
            this.hollowSquareBtn = new ToolStripButton();

            this.panelCanvas = new Panel();
            this.statusStrip = new StatusStrip();
            this.statusLabel = new ToolStripStatusLabel();

            // menuStrip
            this.menuStrip.Items.AddRange(new ToolStripItem[] { this.fileMenu, this.editMenu, this.figureMenu });
            this.menuStrip.Location = new Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new Size(1000, 24);

            // fileMenu
            this.fileMenu.Text = "Файл";
            this.fileMenu.DropDownItems.AddRange(new ToolStripItem[] { this.newMenu, this.openMenu, this.saveMenu, this.exitMenu });

            this.newMenu.Text = "Новый";
            this.newMenu.ShortcutKeys = Keys.Control | Keys.N;
            this.newMenu.Click += New_Click;

            this.openMenu.Text = "Открыть";
            this.openMenu.ShortcutKeys = Keys.Control | Keys.O;
            this.openMenu.Click += Open_Click;

            this.saveMenu.Text = "Сохранить";
            this.saveMenu.ShortcutKeys = Keys.Control | Keys.S;
            this.saveMenu.Click += Save_Click;

            this.exitMenu.Text = "Выход";
            this.exitMenu.Click += Exit_Click;

            // editMenu
            this.editMenu.Text = "Правка";
            this.editMenu.DropDownItems.AddRange(new ToolStripItem[] { this.undoMenu, this.redoMenu, this.copyMenu, this.cutMenu, this.pasteMenu, this.deleteMenu });

            this.undoMenu.Text = "Отменить";
            this.undoMenu.ShortcutKeys = Keys.Control | Keys.Z;
            this.undoMenu.Click += Undo_Click;

            this.redoMenu.Text = "Вернуть";
            this.redoMenu.ShortcutKeys = Keys.Control | Keys.Y;
            this.redoMenu.Click += Redo_Click;

            this.copyMenu.Text = "Копировать";
            this.copyMenu.ShortcutKeys = Keys.Control | Keys.C;
            this.copyMenu.Click += Copy_Click;

            this.cutMenu.Text = "Вырезать";
            this.cutMenu.ShortcutKeys = Keys.Control | Keys.X;
            this.cutMenu.Click += Cut_Click;

            this.pasteMenu.Text = "Вставить";
            this.pasteMenu.ShortcutKeys = Keys.Control | Keys.V;
            this.pasteMenu.Click += Paste_Click;

            this.deleteMenu.Text = "Удалить";
            this.deleteMenu.ShortcutKeys = Keys.Delete;
            this.deleteMenu.Click += Delete_Click;

            // figureMenu
            this.figureMenu.Text = "Фигура";
            this.figureMenu.DropDownItems.AddRange(new ToolStripItem[] { this.rectMenu, this.squareMenu, this.hollowRectMenu, this.hollowSquareMenu, this.holePropsMenu });

            this.rectMenu.Text = "Прямоугольник";
            this.rectMenu.Click += Rect_Click;

            this.squareMenu.Text = "Квадрат";
            this.squareMenu.Click += Square_Click;

            this.hollowRectMenu.Text = "Прямоугольник с отверстием";
            this.hollowRectMenu.Click += HollowRect_Click;

            this.hollowSquareMenu.Text = "Квадрат с отверстием";
            this.hollowSquareMenu.Click += HollowSquare_Click;

            this.holePropsMenu.Text = "Свойства отверстия";
            this.holePropsMenu.Click += HoleProps_Click;

            // toolStrip
            this.toolStrip.Items.AddRange(new ToolStripItem[] {
                this.newBtn, this.openBtn, this.saveBtn, this.sep1,
                this.undoBtn, this.redoBtn, this.sep2,
                this.copyBtn, this.cutBtn, this.pasteBtn, this.sep3,
                this.rectBtn, this.squareBtn, this.hollowRectBtn, this.hollowSquareBtn
            });
            this.toolStrip.Location = new Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new Size(1000, 25);

            this.newBtn.Text = "Новый";
            this.newBtn.Click += New_Click;

            this.openBtn.Text = "Открыть";
            this.openBtn.Click += Open_Click;

            this.saveBtn.Text = "Сохранить";
            this.saveBtn.Click += Save_Click;

            this.undoBtn.Text = "Отменить";
            this.undoBtn.Click += Undo_Click;

            this.redoBtn.Text = "Вернуть";
            this.redoBtn.Click += Redo_Click;

            this.copyBtn.Text = "Копировать";
            this.copyBtn.Click += Copy_Click;

            this.cutBtn.Text = "Вырезать";
            this.cutBtn.Click += Cut_Click;

            this.pasteBtn.Text = "Вставить";
            this.pasteBtn.Click += Paste_Click;

            this.rectBtn.Text = "Прям-к";
            this.rectBtn.Click += Rect_Click;

            this.squareBtn.Text = "Квадрат";
            this.squareBtn.Click += Square_Click;

            this.hollowRectBtn.Text = "Отв.прям";
            this.hollowRectBtn.Click += HollowRect_Click;

            this.hollowSquareBtn.Text = "Отв.кв";
            this.hollowSquareBtn.Click += HollowSquare_Click;

            // panelCanvas
            this.panelCanvas.Dock = DockStyle.Fill;
            this.panelCanvas.BackColor = Color.White;
            this.panelCanvas.Location = new Point(0, 49);
            this.panelCanvas.Name = "panelCanvas";

            // statusStrip
            this.statusStrip.Items.Add(this.statusLabel);
            this.statusStrip.Location = new Point(0, 600);
            this.statusStrip.Name = "statusStrip";

            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Text = "Готов";

            // Form1
            this.ClientSize = new Size(1000, 625);
            this.Controls.Add(this.panelCanvas);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Text = "Векторный редактор - Вариант 9";
            this.WindowState = FormWindowState.Maximized;

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}