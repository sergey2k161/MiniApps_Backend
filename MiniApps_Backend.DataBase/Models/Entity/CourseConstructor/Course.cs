using MiniApps_Backend.DataBase.Models.Entity.ManyToMany;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniApps_Backend.DataBase.Models.Entity.CourseConstructor
{
    /// <summary>
    /// Сущность курса
    /// </summary>
    public class Course
    {
        /// <summary>
        /// Идентификатор курса
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Заголовок курса
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Ссылка на видео о курсе
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
        /// Скидка есть или нет
        /// </summary>
        public bool Discount { get; set; }

        /// <summary>
        /// Цена со скидкой
        /// </summary>
        public decimal? PriceWithDiscount { get; set; }

        /// <summary>
        /// Дата и время создания курса
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// Список уроков
        /// </summary>
        public List<Lesson> Lessons { get; set; } //

        /// <summary>
        /// Опыт за курс
        /// </summary>
        public int Experience { get; set; }

        /// <summary>
        /// Участники курса
        /// </summary>
        public List<User> Users { get; set; }
    }
}

