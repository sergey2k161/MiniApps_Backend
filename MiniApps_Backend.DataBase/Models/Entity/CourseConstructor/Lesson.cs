using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Entity.CourseConstructor
{
    public class Lesson
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string BriefDescription { get; set; }

        public string Description { get; set; }

        public string? UrlVideo { get; set; }

        public Guid CourseId { get; set; } //

        [JsonIgnore]
        public Course Course { get; set; } //

        public int Experience { get; set; }

        public Guid? TestId { get; set; } //
        
        public Test? Test {  get; set; } //
    }
}
