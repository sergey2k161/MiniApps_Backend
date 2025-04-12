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
        public string Title { get; set; }

        /// <summary>
        /// Краткое описание урока
        /// </summary>
        public string BriefDescription { get; set; }

        /// <summary>
        /// Описание урока
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Ссылка на видео урока
        /// </summary>
        public string? UrlVideo { get; set; }

        /// <summary>
        /// Ссылка на курс
        /// </summary>
        public Guid CourseId { get; set; } //
        [JsonIgnore]
        public Course Course { get; set; } //

        /// <summary>
        /// Опыт за урок
        /// </summary>
        public int Experience { get; set; }

        /// <summary>
        /// Ссылка на тест
        /// </summary>
        public Guid? TestId { get; set; } 
        public Test? Test {  get; set; } 
    }
}
