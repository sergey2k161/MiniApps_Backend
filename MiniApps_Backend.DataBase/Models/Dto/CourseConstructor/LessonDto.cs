namespace MiniApps_Backend.DataBase.Models.Dto.CourseConstructor
{
    /// <summary>
    /// Дто урока
    /// </summary>
    public class LessonDto
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Краткое описание
        /// </summary>
        public string BriefDescription { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Ссылка на видео урока
        /// </summary>
        public string? UrlVideo { get; set; }

        /// <summary>
        /// Опыт
        /// </summary>
        public int Experience { get; set; }
    }
}
