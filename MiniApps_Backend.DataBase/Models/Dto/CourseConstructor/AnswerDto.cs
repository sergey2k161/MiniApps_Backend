namespace MiniApps_Backend.DataBase.Models.Dto.CourseConstructor
{
    /// <summary>
    /// Дто для ответов на вопросы теста
    /// </summary>
    public class AnswerDto
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Верность
        /// </summary>
        public bool IsCorrect { get; set; }

        /// <summary>
        /// Обоснование
        /// </summary>
        public string Explanation { get; set; }
    }
}