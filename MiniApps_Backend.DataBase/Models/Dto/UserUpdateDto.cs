namespace MiniApps_Backend.DataBase.Models.Dto
{
    public class UserUpdateDto
    {
        /// <summary>
        /// Идентификатор телеграм пользователя
        /// </summary>
        public long TelegramId { get; set; }
        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Реальное имя
        /// </summary>
        public string RealFirstName { get; set; }

        /// <summary>
        /// Реальная фамилия
        /// </summary>
        public string RealLastName { get; set; }

        /// <summary>
        /// Частота уведомдений в днях
        /// </summary>
        public int NotificationFrequency { get; set; }

        /// <summary>
        /// On/Off Уведомления
        /// </summary>
        public bool TurnNotification { get; set; }
    }
}
