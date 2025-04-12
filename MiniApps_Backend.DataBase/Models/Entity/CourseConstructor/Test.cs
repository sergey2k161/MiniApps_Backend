using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Entity.CourseConstructor
{
    /// <summary>
    /// Сущность теста
    /// </summary>
    public class Test
    {
        /// <summary>
        /// Идентификатор теста
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Ссылка на урок
        /// </summary>
        public Guid LessonId { get; set; }
        [JsonIgnore]
        public Lesson Lesson { get; set; }

        /// <summary>
        /// Список вопросов
        /// </summary>
        public List<Question> Questions {  get; set; }
    }
}
