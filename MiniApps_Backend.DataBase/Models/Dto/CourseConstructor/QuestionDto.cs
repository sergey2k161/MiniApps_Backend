namespace MiniApps_Backend.DataBase.Models.Dto.CourseConstructor
{
    /// <summary>
    /// Дто для вопорса
    /// </summary>
    public class QuestionDto
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Обоснование
        /// </summary>
        public string Explanation { get; set; } = string.Empty;

        /// <summary>
        /// Список отвтеов
        /// </summary>
        public List<AnswerDto>? Answers { get; set; }
    }
}