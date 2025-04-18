namespace MiniApps_Backend.DataBase.Models.Dto.CourseConstructor
{
    /// <summary>
    /// Дто отправки сообщения
    /// </summary>
    public class MaterialRequestDto
    {
        public string TriggerKey { get; set; } = string.Empty;
        public long ChatId { get; set; }
    }
}
