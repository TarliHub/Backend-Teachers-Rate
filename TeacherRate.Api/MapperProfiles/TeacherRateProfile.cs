using AutoMapper;
using TeacherRate.Api.DTOs;
using TeacherRate.Domain.Models;

namespace TeacherRate.Api.MapperProfiles;

public class TeacherRateProfile : Profile
{
    public TeacherRateProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<Teacher, TeacherDTO>()
            .ForMember(dest => dest.Points, act => act.MapFrom(src => src.Points))
            .ReverseMap();
        CreateMap<TaskCategory, TaskCategoryDTO>().ReverseMap();
        CreateMap<UserTask, UserTaskDTO>().ReverseMap();
        CreateMap<CompletedTask, CompletedTaskDTO>()
            .ForMember(dest => dest.TeacherId, act => act.MapFrom(src => src.Teacher.Id))
            .ReverseMap();
        CreateMap<TeacherRequest, TeacherRequestDTO>()
            .ForMember(dest => dest.TaskId, act => act.MapFrom(src => src.Task.Id))
            .ReverseMap();
    }
}
