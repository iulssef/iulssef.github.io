using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProgrammingLanguageQuiz
{
    public partial class Form1 : Form
    {
        private XMLDataManager dataManager;
        private List<Topic> topics;

        public Form1()
        {
            InitializeComponent();
            dataManager = new XMLDataManager();
            LoadTopics();
        }

        /// <summary>
        /// Загрузка списка тем
        /// </summary>
        private void LoadTopics()
        {
            topics = dataManager.LoadTopics();
            cmbTopics.Items.Clear();

            foreach (var topic in topics)
            {
                cmbTopics.Items.Add(topic.Name);
            }

            if (cmbTopics.Items.Count > 0)
            {
                cmbTopics.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Обработчик кнопки "Начать тестирование"
        /// </summary>
        private void btnStartGame_Click(object sender, EventArgs e)
        {
            if (cmbTopics.SelectedItem == null)
            {
                MessageBox.Show("Выберите тему для тестирования!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedTopic = cmbTopics.SelectedItem.ToString();
            GameForm gameForm = new GameForm(selectedTopic, dataManager);
            gameForm.ShowDialog();
        }

        /// <summary>
        /// Обработчик кнопки "Выход"
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}