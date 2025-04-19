using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Entity
{
    public class Support
    {
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Телеграмм Id пользователя, который создал обращение
        /// </summary>
        public long UserTelegramId { get; set; }

        /// <summary>
        /// В работе ли обращение
        /// </summary>
        public bool InWork { get; set; }

        /// <summary>
        /// Статус обращения
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Id помощника, который взял обращение
        /// </summary>
        public long? Helper { get; set; }

        /// <summary>
        /// Под-статус обращения
        /// </summary>
        public string Process { get; set; } = string.Empty;

        /// <summary>
        /// Сообщение пользователя
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
