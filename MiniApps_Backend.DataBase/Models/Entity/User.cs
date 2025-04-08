using System.ComponentModel.DataAnnotations.Schema;

namespace MiniApps_Backend.DataBase.Models.Entity
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid? CommonUserId { get; set; }
        public CommonUser? CommonUser { get; set; }

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
        /// Опыт
        /// </summary>
        public int? Experience { get; set; }

        /// <summary>
        /// Уровень
        /// </summary>
        public int? Level { get; set; }

        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }
    }
}
