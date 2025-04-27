using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Entity.Analysis
{
    /// <summary>
    /// Класс для хранения информации о посещении блока
    /// </summary>
    public class VisitBlock
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор блока
        /// </summary>
        public Guid BlockId { get; set; }

        /// <summary>
        /// Название блока
        /// </summary>
        [JsonIgnore]
        public string BlockTitle { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public long TelegramId { get; set; }

        /// <summary>
        /// Когда был посещен блок
        /// </summary>
        [JsonIgnore]
        public DateTime VisitAt { get; set; }
    }
}
