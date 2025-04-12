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
        public string Title { get; set; }

        /// <summary>
        /// Обоснование
        /// </summary>
        public string Explanation { get; set; }

        /// <summary>
        /// Список отвтеов
        /// </summary>
        public List<AnswerDto> Answers { get; set; }
    }
}