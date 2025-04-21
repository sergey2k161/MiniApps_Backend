using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Entity.CourseConstructor
{
    /// <summary>
    /// Сущность вопроса
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Идентификатор вопроса
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public Guid Id { get; set; }

        /// <summary>
        /// Заголовок вопроса
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Объяснение
        /// </summary>
        public string Explanation { get; set; }

        /// <summary>
        /// Ссылка на тест
        /// </summary>
        //[JsonIgnore]
        public Guid TestId { get; set; }
        //[JsonIgnore]
        public Test Test { get; set; }

        /// <summary>
        /// Список ответов
        /// </summary>
        public List<Answer> Answers { get; set; } 
    }
}

