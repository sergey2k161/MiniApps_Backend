using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Entity.CourseConstructor
{
    /// <summary>
    /// Сущность урока
    /// </summary>
    public class Lesson
    {
        /// <summary>
        /// Идентификатор урока
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Заголовок урока
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Краткое описание урока
        /// </summary>
        public string BriefDescription { get; set; } = string.Empty;

        /// <summary>
        /// Описание урока
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Ссылка на видео урока
        /// </summary>
        public string? UrlVideo { get; set; }

        /// <summary>
        /// Ссылка на курс
        /// </summary>
        public Guid BlockId { get; set; } 

        public Block? Block { get; set; } 

        /// <summary>
        /// Опыт за урок
        /// </summary>
        public int Experience { get; set; }
    }
}
