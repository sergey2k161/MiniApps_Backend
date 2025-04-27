namespace MiniApps_Backend.DataBase.Models.Dto
{
    /// <summary>
    /// Дто для отправки сообщения пользователю
    /// </summary>
    public class UserMessageDto
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text { get; set; } = string.Empty;
    }
}
