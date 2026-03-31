using System.Collections.Generic;

namespace ProgrammingLanguageQuiz
{
    /// <summary>
    /// Класс, представляющий вопрос теста
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Уникальный идентификатор вопроса
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Уровень сложности (1 - начальный, 2 - средний, 3 - сложный)
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Список вариантов ответов
        /// </summary>
        public List<Answer> Answers { get; set; }

        /// <summary>
        /// Пояснение к правильному ответу
        /// </summary>
        public string Explanation { get; set; }

        /// <summary>
        /// Путь к изображению (если есть)
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Question()
        {
            Answers = new List<Answer>();
            Explanation = string.Empty;
            ImagePath = string.Empty;
        }

        /// <summary>
        /// Получить текст правильного ответа
        /// </summary>
        public string GetCorrectAnswerText()
        {
            foreach (var answer in Answers)
            {
                if (answer.IsCorrect)
                    return answer.Text;
            }
            return "Не найден";
        }
    }
}