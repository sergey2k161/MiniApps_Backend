﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniApps_Backend.DataBase.Models.Entity.CourseConstructor
{
    /// <summary>
    /// Идентификатор ответа
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// Идентификатор ответа
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Заголовок ответа
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Верность ответа
        /// </summary>
        public bool IsCorrect { get; set; }

        /// <summary>
        /// Дополнительные данные
        /// </summary>
        public string Explanation { get; set; }

        /// <summary>
        /// Идентификатор вопроса
        /// </summary>
        public Guid QuestionId { get; set; }
        [JsonIgnore]
        public Question Question { get; set; }
    }
}
