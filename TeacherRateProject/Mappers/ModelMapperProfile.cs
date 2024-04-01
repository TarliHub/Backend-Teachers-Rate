using AutoMapper;
using TeacherRateProject.DTOs;
using TeacherRateProject.Models;

namespace TeacherRateProject.Mappers;

public class ModelMapperProfile : Profile
{
    public ModelMapperProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Tasks.Sum(t => t.ActualRating)))
            .ReverseMap(); ;
        CreateMap<RatingTask, RatingTaskDto>();
        CreateMap<CompletedTask, CompletedTaskDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));
        CreateMap<CompletedTaskDto, PostCompletedTaskDto>()
            .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.Task.Id));
        CreateMap<TaskApprove, TaskApproveDto>();
        CreateMap<TaskCategory, TaskCategoryDto>();
    }
}
