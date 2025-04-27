namespace MiniApps_Backend.DataBase.Models.Dto
{
    /// <summary>
    /// Дто для авторизации
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// ключ для авторизации
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public long TelegramId { get; set; }
    }
}
