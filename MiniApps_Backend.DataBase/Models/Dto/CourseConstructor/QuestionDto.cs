namespace MiniApps_Backend.DataBase.Models.Dto.CourseConstructor
{
    public class QuestionDto
    {
        public string Title { get; set; }
        public string Explanation { get; set; }

        public List<AnswerDto> Answers { get; set; }
    }
}