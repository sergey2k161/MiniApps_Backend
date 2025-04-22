namespace MiniApps_Backend.DataBase.Models.Entity
{
    public class TestResultDto
    {
        /// <summary>
        /// Идентификатор телеграмм 
        /// </summary>
        public long TelegramId { get; set; }

        /// <summary>
        /// Реальное имя
        /// </summary>
        public string RealFirstName { get; set; }

        /// <summary>
        /// Реальная фамилия
        /// </summary>
        public string RealLastName { get; set; }

        public string BlockName { get; set; }

        public double PercentageIsTrue { get; set; }

        public int TryNumber { get; set; } = 1;

        /// <summary>
        /// результат последней попытки
        /// </summary>
        public bool Result { get; set; }

    }
}
