namespace MiniApps_Backend.DataBase.Models.Dto.CourseConstructor
{
    /// <summary>
    /// Дто для курса
    /// </summary>
    public class CourseDto
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Ссылка на видео курса
        /// </summary>
        public string? UrlVideo { get; set; }

        /// <summary>
        /// Краткое описание
        /// </summary>
        public string BriefDescription { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Скидка да/нет
        /// </summary>
        public bool Discount { get; set; }

        /// <summary>
        /// Цена со скидкой
        /// </summary>
        public decimal PriceWithDiscount { get; set; }

        /// <summary>
        /// Список уроков
        /// </summary>
        public List<LessonDto> Lessons { get; set; }
    }
}
