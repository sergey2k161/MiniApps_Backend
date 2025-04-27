using AutoMapper;
using MiniApps_Backend.DataBase.Models.Dto.CourseConstructor;
using MiniApps_Backend.DataBase.Models.Entity.CourseConstructor;

namespace MiniApps_Backend.Business.Mapping
{
    /// <summary>
    /// Класс для маппинга между сущностями и DTO
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CourseDto, Course>();
            CreateMap<LessonDto, Lesson>();
            CreateMap<TestDto, Test>();
            CreateMap<QuestionDto, Question>();
            CreateMap<AnswerDto, Answer>();
            CreateMap<BlockDto, Block>();
        }
    }
}
