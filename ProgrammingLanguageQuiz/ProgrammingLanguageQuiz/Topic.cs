using System.Collections.Generic;

namespace ProgrammingLanguageQuiz
{
    /// <summary>
    /// Класс, представляющий тему тестирования
    /// </summary>
    public class Topic
    {
        /// <summary>
        /// Идентификатор темы
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название темы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Вопросы по уровням сложности
        /// Ключ - уровень сложности, Значение - список вопросов
        /// </summary>
        public Dictionary<int, List<Question>> QuestionsByLevel { get; set; }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Topic()
        {
            Name = string.Empty;
            QuestionsByLevel = new Dictionary<int, List<Question>>();
            for (int i = 1; i <= 3; i++)
            {
                QuestionsByLevel[i] = new List<Question>();
            }
        }

        /// <summary>
        /// Получить все вопросы определенного уровня
        /// </summary>
        public List<Question> GetQuestionsByLevel(int level)
        {
            if (QuestionsByLevel.ContainsKey(level))
                return QuestionsByLevel[level];
            return new List<Question>();
        }

        /// <summary>
        /// Добавить вопрос в определенный уровень
        /// </summary>
        public void AddQuestion(Question question, int level)
        {
            if (!QuestionsByLevel.ContainsKey(level))
                QuestionsByLevel[level] = new List<Question>();

            question.Level = level;
            QuestionsByLevel[level].Add(question);
        }
    }
}