using MiniApps_Backend.DataBase.Models.Entity.Ammount;
using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;
using MiniApps_Backend.DataBase.Models.Entity.ManyToMany;
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
        public string? FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Фамилия
        /// </summary>
        public string? LastName { get; set; } = string.Empty;

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

        /// <summary>
        /// Кошелек
        /// </summary>
        public Guid WalletId { get; set; }
        public Wallet Wallet {get; set; }

        /// <summary>
        /// Список курсов
        /// </summary>
        public List<Course> Courses { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Реальное имя
        /// </summary>
        public string RealFirstName { get; set; }

        /// <summary>
        /// Реальная фамилия
        /// </summary>
        public string RealLastName { get; set; }
    }


}
