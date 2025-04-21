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

        public string Title { get; set; } = string.Empty;

        public Guid? TestId { get; set; }
        public Test? Test { get; set; }

        public List<Lesson>? Lessons { get; set; }

        [JsonIgnore]
        public Guid CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
