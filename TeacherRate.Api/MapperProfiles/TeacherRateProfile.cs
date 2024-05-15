﻿using AutoMapper;
using TeacherRate.Api.DTOs;
using TeacherRate.Api.Models.Paging;
using TeacherRate.Domain.Models;

namespace TeacherRate.Api.MapperProfiles;

public class TeacherRateProfile : Profile
{
    public TeacherRateProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<TeacherBase, TeacherBaseDTO>()
            .ForMember(dest => dest.Points, act => act.MapFrom(src => src.Points))
            .ReverseMap();
        CreateMap<TaskCategory, TaskCategoryDTO>().ReverseMap();
        CreateMap<UserTask, UserTaskDTO>()
            .ReverseMap();
        CreateMap<CompletedTask, CompletedTaskDTO>()
            .ForMember(dest => dest.TeacherId, act => act.MapFrom(src => src.Teacher.Id))
            .ReverseMap();
        CreateMap<TeacherRequest, TeacherRequestDTO>()
            .ForMember(dest => dest.TaskId, act => act.MapFrom(src => src.Task.Id))
            .ReverseMap();
        CreateMap<Teacher, TeacherDTO>().ReverseMap();
        CreateMap<Teacher, TeacherWithHeadTeacherDTO>().ReverseMap();
        CreateMap<HeadTeacher, HeadTeacherDTO>().ReverseMap();
        CreateMap<HeadTeacher, HeadTeacherWithTeachersDTO>().ReverseMap();
    }
}
