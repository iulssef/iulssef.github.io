using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace ProgrammingLanguageQuiz
{
    /// <summary>
    /// Класс для работы с XML-файлом
    /// </summary>
    public class XMLDataManager
    {
        private string filePath = "questions.xml";

        /// <summary>
        /// Загрузить все темы из XML-файла
        /// </summary>
        public List<Topic> LoadTopics()
        {
            List<Topic> topics = new List<Topic>();

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл questions.xml не найден!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return topics;
            }

            try
            {
                XDocument doc = XDocument.Load(filePath);

                var topicElements = doc.Descendants("topic");

                foreach (var topicElem in topicElements)
                {
                    Topic topic = new Topic();
                    topic.Id = int.Parse(topicElem.Attribute("id")?.Value ?? "0");
                    topic.Name = topicElem.Attribute("name")?.Value ?? "Без названия";

                    var levelElements = topicElem.Descendants("level");

                    foreach (var levelElem in levelElements)
                    {
                        int levelId = int.Parse(levelElem.Attribute("id")?.Value ?? "1");
                        var questionElements = levelElem.Descendants("question");

                        foreach (var qElem in questionElements)
                        {
                            Question question = new Question();
                            question.Id = int.Parse(qElem.Attribute("id")?.Value ?? "0");
                            question.Text = qElem.Attribute("text")?.Value ?? "";
                            question.Level = levelId;

                            // Загружаем ответы
                            var answerElements = qElem.Descendants("answer");
                            foreach (var aElem in answerElements)
                            {
                                Answer answer = new Answer();
                                answer.Text = aElem.Attribute("text")?.Value ?? "";
                                answer.IsCorrect = (aElem.Attribute("is_correct")?.Value == "true");
                                question.Answers.Add(answer);
                            }

                            // Загружаем пояснение
                            var explanationElem = qElem.Descendants("explanation").FirstOrDefault();
                            if (explanationElem != null)
                            {
                                question.Explanation = explanationElem.Value;
                            }

                            // Загружаем путь к изображению
                            var imagePathElem = qElem.Descendants("image_path").FirstOrDefault();
                            if (imagePathElem != null)
                            {
                                question.ImagePath = imagePathElem.Value;
                            }

                            topic.AddQuestion(question, levelId);
                        }
                    }

                    topics.Add(topic);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return topics;
        }

        /// <summary>
        /// Сохранить темы в XML-файл
        /// </summary>
        public void SaveTopics(List<Topic> topics)
        {
            try
            {
                XDocument doc = new XDocument();
                XElement quizElem = new XElement("quiz");
                XElement topicsElem = new XElement("topics");

                foreach (var topic in topics)
                {
                    XElement topicElem = new XElement("topic",
                        new XAttribute("id", topic.Id),
                        new XAttribute("name", topic.Name));

                    XElement levelsElem = new XElement("levels");

                    for (int level = 1; level <= 3; level++)
                    {
                        XElement levelElem = new XElement("level",
                            new XAttribute("id", level),
                            new XAttribute("name", GetLevelName(level)),
                            new XAttribute("required_score", "80"));

                        XElement questionsElem = new XElement("questions");

                        var questions = topic.GetQuestionsByLevel(level);
                        foreach (var q in questions)
                        {
                            XElement qElem = new XElement("question",
                                new XAttribute("id", q.Id),
                                new XAttribute("text", q.Text));

                            XElement answersElem = new XElement("answers");
                            foreach (var a in q.Answers)
                            {
                                answersElem.Add(new XElement("answer",
                                    new XAttribute("text", a.Text),
                                    new XAttribute("is_correct", a.IsCorrect ? "true" : "false")));
                            }
                            qElem.Add(answersElem);

                            qElem.Add(new XElement("explanation", q.Explanation));
                            qElem.Add(new XElement("image_path", q.ImagePath ?? ""));

                            questionsElem.Add(qElem);
                        }

                        levelElem.Add(questionsElem);
                        levelsElem.Add(levelElem);
                    }

                    topicElem.Add(levelsElem);
                    topicsElem.Add(topicElem);
                }

                quizElem.Add(topicsElem);
                doc.Add(quizElem);
                doc.Save(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Получить название уровня по его номеру
        /// </summary>
        private string GetLevelName(int level)
        {
            switch (level)
            {
                case 1: return "Начальный";
                case 2: return "Средний";
                case 3: return "Сложный";
                default: return "Уровень " + level;
            }
        }

        /// <summary>
        /// Получить список названий тем
        /// </summary>
        public List<string> GetTopicNames()
        {
            List<string> names = new List<string>();
            var topics = LoadTopics();
            foreach (var topic in topics)
            {
                names.Add(topic.Name);
            }
            return names;
        }

        /// <summary>
        /// Получить тему по названию
        /// </summary>
        public Topic GetTopicByName(string name)
        {
            var topics = LoadTopics();
            return topics.FirstOrDefault(t => t.Name == name);
        }
    }
}