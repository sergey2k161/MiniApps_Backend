namespace MiniApps_Backend.DataBase.Models.Dto
{
    /// <summary>
    /// Класс для получения удобного ответа
    /// </summary>
    public class ResultDto
    {
        public bool IsSuccess { get; set; }
        public List<string>? Errors { get; set; }

        /// <summary>
        /// Ответ на ошибку
        /// </summary>
        /// <param name="errors">Список ошибок</param>
        public ResultDto(List<string> errors)
        {
            IsSuccess = false;
            Errors = errors;
        }

        /// <summary>
        /// Ответ об успешном выполнении
        /// </summary>
        public ResultDto()
        {
            IsSuccess = true;
        }
    }
}
