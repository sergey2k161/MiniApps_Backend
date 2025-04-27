using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Dto.CourseConstructor
{
    /// <summary>
    /// Дто блока
    /// </summary>
    public class BlockDto
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Тест
        /// </summary>
        public TestDto? Test { get; set; }

        /// <summary>
        /// Список уроков
        /// </summary>
        public List<LessonDto>? Lessons { get; set; }

        /// <summary>
        /// Идентификатор курса
        /// </summary>
        [JsonIgnore]
        public Guid CourseId { get; set; }

        /// <summary>
        /// Номер блока
        /// </summary>
        public int NumberOfBLock { get; set; }
    }
}
