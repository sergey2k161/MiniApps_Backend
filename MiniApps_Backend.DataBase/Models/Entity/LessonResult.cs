using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Entity
{
    /// <summary>
    /// Сущность завершения урока
    /// </summary>
    public class LessonResult
    {
        /// <summary>
        /// Идентификатор 
        /// </summary>
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор телеграмм
        /// </summary>
        public long TelegramId { get; set; }
        
        /// <summary>
        /// Идентификатор урока
        /// </summary>
        public Guid LessonId { get; set; }

        /// <summary>
        /// Когда урок был завершен
        /// </summary>
        [JsonIgnore]
        public DateTime WhenСompleted { get; set; }
    }
}
