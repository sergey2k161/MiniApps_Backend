using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Dto.CourseConstructor
{
    public class CourseMaterial
    {
        /// <summary>
        /// Идентификатор материала для урока
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public Guid Id { get; set; }

        /// <summary>
        /// Тригер фраза
        /// </summary>
        public string TriggerKey { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор чата
        /// </summary>
        public long TelegramChatId { get; set; }

        /// <summary>
        /// Идентификатор сообщения
        /// </summary>
        public int TelegramMessageId { get; set; }
    }
}
