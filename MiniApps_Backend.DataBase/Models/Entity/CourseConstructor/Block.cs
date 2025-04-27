using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Entity.CourseConstructor
{
    public class Block
    {
        /// <summary>
        /// Идентификатор блока
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public Guid Id { get; set; }

        /// <summary>
        /// Название блока
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор теста
        /// </summary>
        public Guid? TestId { get; set; }
        public Test? Test { get; set; }

        /// <summary>
        /// Список уроков в блоке
        /// </summary>
        public List<Lesson>? Lessons { get; set; }

        /// <summary>
        /// Идентификатор курса
        /// </summary>
        [JsonIgnore]
        public Guid CourseId { get; set; }
        public Course? Course { get; set; }

        /// <summary>
        /// Номер блока
        /// </summary>
        public int NumberOfBLock { get; set; }
    }
}
