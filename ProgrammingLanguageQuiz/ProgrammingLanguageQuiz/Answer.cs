namespace ProgrammingLanguageQuiz
{
    /// <summary>
    /// Класс, представляющий вариант ответа
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// Текст ответа
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Является ли ответ правильным
        /// </summary>
        public bool IsCorrect { get; set; }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Answer()
        {
            Text = string.Empty;
            IsCorrect = false;
        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        public Answer(string text, bool isCorrect)
        {
            Text = text;
            IsCorrect = isCorrect;
        }
    }
}