namespace MiniApps_Backend.DataBase.Models.Dto
{
    /// <summary>
    /// Модель для получения информации о пользователе
    /// </summary>
    public class UserRequest
    {
        /// <summary>
        /// Идентификатор Telegram
        /// </summary>
        public long TelegramId { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// реальное имя
        /// </summary>
        public string RealFirstName { get; set; }

        /// <summary>
        /// Реальная фамилия
        /// </summary>
        public string RealLastName { get; set; }
    }
}
