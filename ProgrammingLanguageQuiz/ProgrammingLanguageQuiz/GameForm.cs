using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProgrammingLanguageQuiz
{
    public partial class GameForm : Form
    {
        private XMLDataManager dataManager;
        private Topic currentTopic;
        private List<Question> currentQuestions;
        private int currentQuestionIndex;
        private int currentLevel;
        private int score;
        private int timeRemaining;
        private Timer gameTimer;
        private bool answerSubmitted;
        private Random random;
        private int totalQuestionsPerSession = 5;

        // Массив радио-кнопок для удобного доступа
        private RadioButton[] radioButtons;

        public GameForm(string topicName, XMLDataManager manager)
        {
            dataManager = manager;
            currentTopic = dataManager.GetTopicByName(topicName);
            currentLevel = 1;
            score = 0;
            random = new Random();
            answerSubmitted = false;

            InitializeComponent();

            // Инициализация массива радио-кнопок
            radioButtons = new RadioButton[] { rbOption1, rbOption2, rbOption3, rbOption4 };

            LoadQuestionsForCurrentLevel();
            StartNewSession();
        }

        /// <summary>
        /// Загрузка вопросов для текущего уровня
        /// </summary>
        private void LoadQuestionsForCurrentLevel()
        {
            var allQuestions = currentTopic.GetQuestionsByLevel(currentLevel);
            // Выбираем случайные вопросы
            currentQuestions = allQuestions.OrderBy(x => random.Next()).Take(totalQuestionsPerSession).ToList();
            currentQuestionIndex = 0;
        }

        /// <summary>
        /// Начало новой сессии тестирования
        /// </summary>
        private void StartNewSession()
        {
            answerSubmitted = false;
            UpdateScoreDisplay();
            UpdateLevelDisplay();

            // Сброс радио-кнопок
            foreach (var rb in radioButtons)
            {
                rb.Checked = false;
                rb.Visible = true;
            }

            // Запуск таймера (60 секунд)
            timeRemaining = 60;
            gameTimer = new Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += Timer_Tick;
            gameTimer.Start();

            // Показ первого вопроса
            ShowCurrentQuestion();
        }

        /// <summary>
        /// Обновление отображения счета
        /// </summary>
        private void UpdateScoreDisplay()
        {
            lblScore.Text = $"Счёт: {score}";
        }

        /// <summary>
        /// Обновление отображения уровня
        /// </summary>
        private void UpdateLevelDisplay()
        {
            string levelName = "";
            switch (currentLevel)
            {
                case 1: levelName = "Начальный"; break;
                case 2: levelName = "Средний"; break;
                case 3: levelName = "Сложный"; break;
            }
            lblLevel.Text = $"Уровень: {currentLevel} ({levelName})";
        }

        /// <summary>
        /// Отображение текущего вопроса
        /// </summary>
        private void ShowCurrentQuestion()
        {
            if (currentQuestionIndex >= currentQuestions.Count)
            {
                EndSession();
                return;
            }

            Question q = currentQuestions[currentQuestionIndex];
            lblQuestion.Text = $"{currentQuestionIndex + 1}. {q.Text}";

            // Отображение вариантов ответов
            for (int i = 0; i < q.Answers.Count && i < radioButtons.Length; i++)
            {
                radioButtons[i].Text = q.Answers[i].Text;
                radioButtons[i].Checked = false;
                radioButtons[i].Visible = true;
            }

            // Скрытие лишних радио-кнопок
            for (int i = q.Answers.Count; i < radioButtons.Length; i++)
            {
                radioButtons[i].Visible = false;
            }

            // Отображение изображения - ИСПРАВЛЕНО: используем полное имя System.Drawing.Image
            if (!string.IsNullOrEmpty(q.ImagePath) && System.IO.File.Exists(q.ImagePath))
            {
                pbImage.Image = System.Drawing.Image.FromFile(q.ImagePath);
                pbImage.Visible = true;
            }
            else
            {
                pbImage.Image = null;
                pbImage.Visible = false;
            }

            // Скрытие пояснения
            lblExplanation.Visible = false;
            answerSubmitted = false;
            btnSubmit.Enabled = true;
            btnNext.Enabled = false;
        }

        /// <summary>
        /// Обработчик таймера
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            timeRemaining--;
            lblTimer.Text = $"Время: {timeRemaining}";

            if (timeRemaining <= 0)
            {
                gameTimer.Stop();
                MessageBox.Show("Время вышло! Тестирование завершено.", "Время истекло",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EndSession();
            }
        }

        /// <summary>
        /// Обработчик кнопки "Ответить"
        /// </summary>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (answerSubmitted)
            {
                MessageBox.Show("Вы уже ответили на этот вопрос!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Поиск выбранного ответа
            int selectedIndex = -1;
            for (int i = 0; i < radioButtons.Length; i++)
            {
                if (radioButtons[i].Visible && radioButtons[i].Checked)
                {
                    selectedIndex = i;
                    break;
                }
            }

            if (selectedIndex == -1)
            {
                MessageBox.Show("Выберите вариант ответа!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Question q = currentQuestions[currentQuestionIndex];
            bool isCorrect = q.Answers[selectedIndex].IsCorrect;

            if (isCorrect)
            {
                score += 20;
                MessageBox.Show("✅ ПРАВИЛЬНО! +20 баллов", "Отлично!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string correctAnswer = q.GetCorrectAnswerText();
                MessageBox.Show($"❌ НЕПРАВИЛЬНО!\n\nПравильный ответ: {correctAnswer}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Показ пояснения
            if (!string.IsNullOrEmpty(q.Explanation))
            {
                lblExplanation.Text = $"📖 Пояснение: {q.Explanation}";
                lblExplanation.Visible = true;
            }

            answerSubmitted = true;
            btnSubmit.Enabled = false;
            btnNext.Enabled = true;
            UpdateScoreDisplay();
        }

        /// <summary>
        /// Обработчик кнопки "Следующий"
        /// </summary>
        private void btnNext_Click(object sender, EventArgs e)
        {
            currentQuestionIndex++;

            if (currentQuestionIndex < currentQuestions.Count)
            {
                ShowCurrentQuestion();
            }
            else
            {
                EndSession();
            }
        }

        /// <summary>
        /// Завершение сессии тестирования
        /// </summary>
        private void EndSession()
        {
            if (gameTimer != null)
            {
                gameTimer.Stop();
                gameTimer.Dispose();
            }

            int maxScore = currentQuestions.Count * 20;
            double percentage = (double)score / maxScore * 100;

            DialogResult result = MessageBox.Show(
                $"Тестирование завершено!\n\n" +
                $"Ваш счёт: {score} из {maxScore}\n" +
                $"Процент правильных ответов: {percentage:F1}%\n\n" +
                $"Требуется 80% для перехода на следующий уровень.\n\n" +
                $"{(percentage >= 80 ? "Поздравляем! Вы прошли уровень!" : "К сожалению, вы не набрали достаточно баллов.")}\n\n" +
                $"Хотите пройти тест заново?",
                "Результаты тестирования",
                MessageBoxButtons.YesNo,
                percentage >= 80 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // Повторное прохождение текущего уровня
                LoadQuestionsForCurrentLevel();
                currentQuestionIndex = 0;
                score = 0;
                StartNewSession();
            }
            else
            {
                // Проверка возможности перехода на следующий уровень
                if (percentage >= 80 && currentLevel < 3)
                {
                    DialogResult nextLevel = MessageBox.Show(
                        $"Вы набрали {percentage:F1}%!\n\n" +
                        $"Желаете перейти на следующий уровень сложности?",
                        "Переход на следующий уровень",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (nextLevel == DialogResult.Yes)
                    {
                        currentLevel++;
                        LoadQuestionsForCurrentLevel();
                        currentQuestionIndex = 0;
                        score = 0;
                        StartNewSession();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Обработчик закрытия формы
        /// </summary>
        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (gameTimer != null)
            {
                gameTimer.Stop();
                gameTimer.Dispose();
            }
        }

        /// <summary>
        /// Обработчик кнопки "Выход"
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Вы уверены, что хотите выйти? Прогресс будет потерян.",
                "Подтверждение выхода",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void rbOption3_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}