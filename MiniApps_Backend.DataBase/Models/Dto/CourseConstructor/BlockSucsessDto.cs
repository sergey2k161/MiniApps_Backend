using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Dto.CourseConstructor
{
    /// <summary>
    /// Дто для успешного завершения блока
    /// </summary>
    public class BlockSucsessDto
    {
        /// <summary>
        /// идентификатор
        /// </summary>
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public long TelegramId { get; set; }

        /// <summary>
        /// Идентификатор блока
        /// </summary>
        public Guid BlockId { get; set; }

        /// <summary>
        /// Когда был завершен блок
        /// </summary>
        public DateTime? FinishAt { get; set; } = null;

        /// <summary>
        /// Завершен ли блок
        /// </summary>
        public bool Finish { get; set; }
    }
}
