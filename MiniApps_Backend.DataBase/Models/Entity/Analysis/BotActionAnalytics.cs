using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Entity.Analysis
{
    /// <summary>
    /// Класс для хранения информации о действиях бота
    /// </summary>
    public class BotActionAnalytics
    {
        /// <summary>
        /// Идентификатор 
        /// </summary>
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Название действия
        /// </summary>
        public string ActionName { get; set; } = string.Empty;

        /// <summary>
        /// Дата и время выполнения действия
        /// </summary>
        public DateTime ExecutedAt { get; set; }

        /// <summary>
        /// Результат выполнения действия
        /// </summary>
        public string Result { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public long? UserId { get; set; } 

    }
}
