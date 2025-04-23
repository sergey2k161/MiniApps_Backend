using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Dto.CourseConstructor
{
    public class BlockDto
    {
        public string Title { get; set; } = string.Empty;

        public TestDto? Test { get; set; }

        public List<LessonDto>? Lessons { get; set; }

        [JsonIgnore]
        public Guid CourseId { get; set; }

        public int NumberOfBLock { get; set; }
    }
}
