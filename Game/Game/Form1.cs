using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace PicturePuzzleGame
{
    public partial class Form1 : Form
    {
        // Константы игры
        private const int GRID_SIZE = 4;
        private const int TILE_SIZE = 100;
        private const int EMPTY_TILE = 15;

        // Переменные игры
        private Button[,] tiles;
        private int[,] tileNumbers;
        private int emptyRow, emptyCol;
        private Timer gameTimer;
        private int timeLeft;
        private string currentPlayer;
        private Image originalImage;
        private Image[] tileImages;
        private int movesCount;

        // Элементы управления
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileMenu;
        private ToolStripMenuItem loadImageMenuItem;
        private ToolStripMenuItem exitMenuItem;
        private ToolStripMenuItem settingsMenu;
        private ToolStripMenuItem difficultyMenuItem;
        private ToolStripMenuItem easyMenuItem;
        private ToolStripMenuItem mediumMenuItem;
        private ToolStripMenuItem hardMenuItem;
        private ToolStripMenuItem helpMenu;
        private ToolStripMenuItem aboutMenuItem;
        private ToolStripMenuItem resultsMenuItem;
        private Panel gamePanel;
        private Label timerLabel;
        private Label playerLabel;
        private Label movesLabel;
        private Button newGameButton;
        private Button shuffleButton;
        private TextBox playerNameTextBox;
        private Label statusLabel;

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeComponent()
        {
            // Настройка формы
            this.Text = "Собери картинку - Игра \"15\"";
            this.Size = new Size(500, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

            // MenuStrip
            menuStrip = new MenuStrip();

            // Файл меню
            fileMenu = new ToolStripMenuItem("Файл");
            loadImageMenuItem = new ToolStripMenuItem("Загрузить картинку");
            loadImageMenuItem.Click += LoadImageMenuItem_Click;
            exitMenuItem = new ToolStripMenuItem("Выход");
            exitMenuItem.Click += (s, e) => Application.Exit();
            fileMenu.DropDownItems.Add(loadImageMenuItem);
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add(exitMenuItem);

            // Настройки меню
            settingsMenu = new ToolStripMenuItem("Настройки");
            difficultyMenuItem = new ToolStripMenuItem("Уровень сложности");
            easyMenuItem = new ToolStripMenuItem("Лёгкий (без времени)");
            mediumMenuItem = new ToolStripMenuItem("Средний (5 минут)");
            hardMenuItem = new ToolStripMenuItem("Сложный (2 минуты)");
            easyMenuItem.Click += SetDifficulty;
            mediumMenuItem.Click += SetDifficulty;
            hardMenuItem.Click += SetDifficulty;
            difficultyMenuItem.DropDownItems.Add(easyMenuItem);
            difficultyMenuItem.DropDownItems.Add(mediumMenuItem);
            difficultyMenuItem.DropDownItems.Add(hardMenuItem);
            settingsMenu.DropDownItems.Add(difficultyMenuItem);

            // Справка меню
            helpMenu = new ToolStripMenuItem("Справка");
            aboutMenuItem = new ToolStripMenuItem("О программе");
            aboutMenuItem.Click += AboutMenuItem_Click;
            resultsMenuItem = new ToolStripMenuItem("Результаты");
            resultsMenuItem.Click += ResultsMenuItem_Click;
            helpMenu.DropDownItems.Add(aboutMenuItem);
            helpMenu.DropDownItems.Add(resultsMenuItem);

            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(settingsMenu);
            menuStrip.Items.Add(helpMenu);

            // Game Panel
            gamePanel = new Panel();
            gamePanel.Location = new Point(25, 100);
            gamePanel.Size = new Size(GRID_SIZE * TILE_SIZE, GRID_SIZE * TILE_SIZE);
            gamePanel.BackColor = Color.LightGray;
            gamePanel.BorderStyle = BorderStyle.FixedSingle;

            // Player Label
            playerLabel = new Label();
            playerLabel.Text = "Игрок:";
            playerLabel.Location = new Point(25, 45);
            playerLabel.Size = new Size(50, 25);

            // Player TextBox
            playerNameTextBox = new TextBox();
            playerNameTextBox.Text = "Игрок";
            playerNameTextBox.Location = new Point(80, 42);
            playerNameTextBox.Size = new Size(120, 25);

            // Timer Label
            timerLabel = new Label();
            timerLabel.Text = "Время: --:--";
            timerLabel.Location = new Point(220, 45);
            timerLabel.Size = new Size(100, 25);
            timerLabel.Font = new Font("Arial", 10, FontStyle.Bold);

            // Moves Label
            movesLabel = new Label();
            movesLabel.Text = "Ходов: 0";
            movesLabel.Location = new Point(340, 45);
            movesLabel.Size = new Size(100, 25);
            movesLabel.Font = new Font("Arial", 10, FontStyle.Bold);

            // New Game Button
            newGameButton = new Button();
            newGameButton.Text = "Новая игра";
            newGameButton.Location = new Point(25, 520);
            newGameButton.Size = new Size(120, 35);
            newGameButton.BackColor = Color.LightGreen;
            newGameButton.FlatStyle = FlatStyle.Flat;
            newGameButton.Click += NewGameButton_Click;

            // Shuffle Button
            shuffleButton = new Button();
            shuffleButton.Text = "Перемешать";
            shuffleButton.Location = new Point(160, 520);
            shuffleButton.Size = new Size(120, 35);
            shuffleButton.BackColor = Color.LightYellow;
            shuffleButton.FlatStyle = FlatStyle.Flat;
            shuffleButton.Click += ShuffleButton_Click;

            // Status Label
            statusLabel = new Label();
            statusLabel.Text = "Загрузите картинку и нажмите 'Перемешать'";
            statusLabel.Location = new Point(25, 570);
            statusLabel.Size = new Size(450, 30);
            statusLabel.ForeColor = Color.Blue;

            // Добавление элементов на форму
            this.Controls.Add(menuStrip);
            this.Controls.Add(gamePanel);
            this.Controls.Add(playerLabel);
            this.Controls.Add(playerNameTextBox);
            this.Controls.Add(timerLabel);
            this.Controls.Add(movesLabel);
            this.Controls.Add(newGameButton);
            this.Controls.Add(shuffleButton);
            this.Controls.Add(statusLabel);

            // Таймер
            gameTimer = new Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
        }

        private void InitializeGame()
        {
            tiles = new Button[GRID_SIZE, GRID_SIZE];
            tileNumbers = new int[GRID_SIZE, GRID_SIZE];
            tileImages = new Image[GRID_SIZE * GRID_SIZE];

            CreateDefaultImage();
            CreateTileImages();

            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    Button btn = new Button();
                    btn.Size = new Size(TILE_SIZE - 2, TILE_SIZE - 2);
                    btn.Location = new Point(j * TILE_SIZE + 1, i * TILE_SIZE + 1);
                    btn.Tag = new Point(i, j);
                    btn.BackColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.Click += Tile_Click;
                    tiles[i, j] = btn;
                    gamePanel.Controls.Add(btn);
                }
            }

            InitializeBoard();
        }

        private void CreateDefaultImage()
        {
            originalImage = new Bitmap(GRID_SIZE * TILE_SIZE, GRID_SIZE * TILE_SIZE);
            using (Graphics g = Graphics.FromImage(originalImage))
            {
                g.Clear(Color.LightBlue);
                for (int i = 0; i < GRID_SIZE; i++)
                {
                    for (int j = 0; j < GRID_SIZE; j++)
                    {
                        Color color = Color.FromArgb(100 + i * 30, 100 + j * 30, 180);
                        using (Brush brush = new SolidBrush(color))
                        {
                            g.FillRectangle(brush, j * TILE_SIZE, i * TILE_SIZE, TILE_SIZE, TILE_SIZE);
                        }
                        g.DrawRectangle(Pens.Black, j * TILE_SIZE, i * TILE_SIZE, TILE_SIZE, TILE_SIZE);
                    }
                }
                string text = "Загрузите\nсвою\nкартинку!";
                Font font = new Font("Arial", 12, FontStyle.Bold);
                SizeF textSize = g.MeasureString(text, font);
                g.DrawString(text, font, Brushes.White,
                    (GRID_SIZE * TILE_SIZE - textSize.Width) / 2,
                    (GRID_SIZE * TILE_SIZE - textSize.Height) / 2);
            }
        }

        private void CreateTileImages()
        {
            if (originalImage == null) return;

            int tileWidth = originalImage.Width / GRID_SIZE;
            int tileHeight = originalImage.Height / GRID_SIZE;

            for (int i = 0; i < GRID_SIZE * GRID_SIZE; i++)
            {
                int row = i / GRID_SIZE;
                int col = i % GRID_SIZE;
                tileImages[i] = new Bitmap(tileWidth, tileHeight);
                using (Graphics g = Graphics.FromImage(tileImages[i]))
                {
                    g.DrawImage(originalImage,
                        new Rectangle(0, 0, tileWidth, tileHeight),
                        new Rectangle(col * tileWidth, row * tileHeight, tileWidth, tileHeight),
                        GraphicsUnit.Pixel);
                    g.DrawRectangle(Pens.Black, 0, 0, tileWidth - 1, tileHeight - 1);
                }
            }
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    tileNumbers[i, j] = i * GRID_SIZE + j;
                }
            }

            emptyRow = GRID_SIZE - 1;
            emptyCol = GRID_SIZE - 1;
            tileNumbers[emptyRow, emptyCol] = EMPTY_TILE;
            movesCount = 0;
            UpdateMovesLabel();
            UpdateBoardDisplay();
        }

        private void UpdateBoardDisplay()
        {
            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    int number = tileNumbers[i, j];
                    if (number == EMPTY_TILE)
                    {
                        tiles[i, j].Text = "";
                        tiles[i, j].BackgroundImage = null;
                        tiles[i, j].BackColor = Color.LightGray;
                    }
                    else
                    {
                        tiles[i, j].Text = "";
                        if (number >= 0 && number < tileImages.Length && tileImages[number] != null)
                        {
                            tiles[i, j].BackgroundImage = tileImages[number];
                            tiles[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            tiles[i, j].BackColor = Color.White;
                        }
                    }
                }
            }
        }

        private void Tile_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Point pos = (Point)btn.Tag;
            int row = pos.X;
            int col = pos.Y;

            if ((Math.Abs(row - emptyRow) + Math.Abs(col - emptyCol)) == 1)
            {
                tileNumbers[emptyRow, emptyCol] = tileNumbers[row, col];
                tileNumbers[row, col] = EMPTY_TILE;
                emptyRow = row;
                emptyCol = col;

                movesCount++;
                UpdateMovesLabel();
                UpdateBoardDisplay();

                if (CheckWin())
                {
                    GameWin();
                }
            }
        }

        private bool CheckWin()
        {
            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    if (i == GRID_SIZE - 1 && j == GRID_SIZE - 1)
                    {
                        if (tileNumbers[i, j] != EMPTY_TILE)
                            return false;
                    }
                    else
                    {
                        if (tileNumbers[i, j] != i * GRID_SIZE + j)
                            return false;
                    }
                }
            }
            return true;
        }

        private void GameWin()
        {
            gameTimer.Stop();
            statusLabel.Text = $"ПОБЕДА! {currentPlayer}, вы собрали картинку за {movesCount} ходов!";
            statusLabel.ForeColor = Color.Green;
            MessageBox.Show($"Поздравляю, {currentPlayer}!\nВы собрали картинку за {movesCount} ходов!\nВремя: {FormatTime(timeLeft)}",
                "Победа!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SaveResult(true);
        }

        private void GameLose()
        {
            gameTimer.Stop();
            statusLabel.Text = $"Время вышло! {currentPlayer}, вы проиграли!";
            statusLabel.ForeColor = Color.Red;
            MessageBox.Show($"Время вышло, {currentPlayer}!\nПопробуйте снова.",
                "Время истекло", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            SaveResult(false);
        }

        private void ShuffleBoard()
        {
            Random rand = new Random();
            for (int move = 0; move < 200; move++)
            {
                var neighbors = GetNeighborCells(emptyRow, emptyCol);
                if (neighbors.Count > 0)
                {
                    var neighbor = neighbors[rand.Next(neighbors.Count)];
                    tileNumbers[emptyRow, emptyCol] = tileNumbers[neighbor.Row, neighbor.Col];
                    tileNumbers[neighbor.Row, neighbor.Col] = EMPTY_TILE;
                    emptyRow = neighbor.Row;
                    emptyCol = neighbor.Col;
                }
            }
            movesCount = 0;
            UpdateMovesLabel();
            UpdateBoardDisplay();
        }

        private List<(int Row, int Col)> GetNeighborCells(int row, int col)
        {
            var neighbors = new List<(int, int)>();
            if (row > 0) neighbors.Add((row - 1, col));
            if (row < GRID_SIZE - 1) neighbors.Add((row + 1, col));
            if (col > 0) neighbors.Add((row, col - 1));
            if (col < GRID_SIZE - 1) neighbors.Add((row, col + 1));
            return neighbors;
        }

        private void SetDifficulty(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (item == easyMenuItem)
            {
                gameTimer.Stop();
                timeLeft = 0;
                timerLabel.Text = "Время: --:--";
                statusLabel.Text = "Лёгкий уровень (без ограничения времени)";
            }
            else if (item == mediumMenuItem)
            {
                timeLeft = 300;
                timerLabel.Text = $"Время: {FormatTime(timeLeft)}";
                statusLabel.Text = "Средний уровень (5 минут)";
            }
            else if (item == hardMenuItem)
            {
                timeLeft = 120;
                timerLabel.Text = $"Время: {FormatTime(timeLeft)}";
                statusLabel.Text = "Сложный уровень (2 минуты)";
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
                timerLabel.Text = $"Время: {FormatTime(timeLeft)}";
                if (timeLeft == 0)
                {
                    GameLose();
                }
            }
        }

        private string FormatTime(int seconds)
        {
            if (seconds <= 0) return "00:00";
            return $"{seconds / 60:D2}:{seconds % 60:D2}";
        }

        private void UpdateMovesLabel()
        {
            movesLabel.Text = $"Ходов: {movesCount}";
        }

        private void LoadImageMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Выберите картинку для игры";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        originalImage = Image.FromFile(openFileDialog.FileName);
                        originalImage = new Bitmap(originalImage, GRID_SIZE * TILE_SIZE, GRID_SIZE * TILE_SIZE);
                        CreateTileImages();
                        InitializeBoard();
                        statusLabel.Text = "Картинка загружена! Нажмите 'Перемешать' для начала игры.";
                        statusLabel.ForeColor = Color.Green;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            currentPlayer = playerNameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(currentPlayer))
            {
                currentPlayer = "Игрок";
                playerNameTextBox.Text = "Игрок";
            }

            InitializeBoard();
            movesCount = 0;
            UpdateMovesLabel();

            if (timeLeft > 0)
            {
                gameTimer.Stop();
                gameTimer.Start();
            }

            statusLabel.Text = $"Игра началась! {currentPlayer}, удачи!";
            statusLabel.ForeColor = Color.Blue;
        }

        private void ShuffleButton_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Сначала загрузите картинку через меню 'Файл' -> 'Загрузить картинку'",
                    "Нет картинки", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ShuffleBoard();
            statusLabel.Text = "Плитки перемешаны! Начинайте собирать картинку!";
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Игра 'Собери картинку' (аналог игры '15')\n\n" +
                "Правила игры:\n" +
                "• Перемещайте плитки, нажимая на них\n" +
                "• Используйте пустую ячейку для перемещения\n" +
                "• Соберите полную картинку\n\n" +
                "Вариант 9 курсовой работы",
                "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ResultsMenuItem_Click(object sender, EventArgs e)
        {
            ShowResultsForm();
        }

        private void SaveResult(bool isWin)
        {
            GameResult result = new GameResult
            {
                PlayerName = currentPlayer,
                Moves = movesCount,
                TimeLeft = timeLeft,
                IsWin = isWin,
                GameDate = DateTime.Now,
                Difficulty = GetCurrentDifficulty()
            };

            SaveResultToFile(result);
        }

        private string GetCurrentDifficulty()
        {
            if (timeLeft == 0) return "Лёгкий";
            if (timeLeft == 300) return "Средний";
            if (timeLeft == 120) return "Сложный";
            return "Неизвестно";
        }

        private void SaveResultToFile(GameResult result)
        {
            string filePath = "game_results.dat";
            List<GameResult> results = new List<GameResult>();

            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    results = (List<GameResult>)formatter.Deserialize(fs);
                }
            }

            results.Add(result);

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, results);
            }
        }

        private void ShowResultsForm()
        {
            Form resultsForm = new Form();
            resultsForm.Text = "Результаты игроков";
            resultsForm.Size = new Size(500, 400);
            resultsForm.StartPosition = FormStartPosition.CenterParent;

            ListBox listBox = new ListBox();
            listBox.Dock = DockStyle.Fill;
            listBox.Font = new Font("Consolas", 10);

            string filePath = "game_results.dat";
            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    List<GameResult> results = (List<GameResult>)formatter.Deserialize(fs);

                    var playerResults = results.FindAll(r => r.PlayerName == currentPlayer);

                    if (playerResults.Count == 0)
                    {
                        listBox.Items.Add("Нет сохранённых результатов для игрока " + currentPlayer);
                    }
                    else
                    {
                        listBox.Items.Add("=== Результаты игрока " + currentPlayer + " ===");
                        listBox.Items.Add("");
                        foreach (var r in playerResults)
                        {
                            string status = r.IsWin ? "ПОБЕДА" : "ПОРАЖЕНИЕ";
                            listBox.Items.Add($"{r.GameDate:dd.MM.yyyy HH:mm:ss} | {status} | Ходов: {r.Moves} | {r.Difficulty}");
                        }
                    }
                }
            }
            else
            {
                listBox.Items.Add("Нет сохранённых результатов");
            }

            resultsForm.Controls.Add(listBox);
            resultsForm.ShowDialog();
        }
    }

    [Serializable]
    public class GameResult
    {
        public string PlayerName { get; set; }
        public int Moves { get; set; }
        public int TimeLeft { get; set; }
        public bool IsWin { get; set; }
        public DateTime GameDate { get; set; }
        public string Difficulty { get; set; }
    }
}