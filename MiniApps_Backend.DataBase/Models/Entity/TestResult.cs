using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Entity
{
    /// <summary>
    /// Сущность результата теста
    /// </summary>
    public class TestResult
    {
        /// <summary>
        /// идентификатор результата
        /// </summary>
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }    

        /// <summary>
        /// Идентификатор телеграмм 
        /// </summary>
        public long TelegramId { get; set; }

        /// <summary>
        /// Идентификатор теста
        /// </summary>
        public Guid TestId { get; set; }

        /// <summary>
        /// Последняя попытка
        /// </summary>
        [JsonIgnore]
        public DateTime LastTry { get; set; }

        [JsonIgnore]
        public int TryNumber { get; set; } = 1;

        /// <summary>
        /// результат последней попытки
        /// </summary>
        public bool Result { get; set; }

        public double PercentageIsTrue { get; set; }
    }
}
