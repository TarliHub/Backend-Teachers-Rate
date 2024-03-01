using AutoMapper;
using TeacherRateProject.DTOs;
using TeacherRateProject.Models;

namespace TeacherRateProject.Mappers;

public class ModelMapperProfile : Profile
{
    public ModelMapperProfile()
    {
        CreateMap<User, UserDto>();
    }
}
