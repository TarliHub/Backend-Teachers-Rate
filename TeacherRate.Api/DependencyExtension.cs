using AutoMapper;
using TeacherRate.Api.MapperProfiles;
using TeacherRate.Api.Services;
using TeacherRate.Api.Services.Base;
using TeacherRate.Domain.Interfaces;
using TeacherRate.Domain.Interfaces.Base;
using TeacherRate.Storage.Abstraction.Interfaces;
using TeacherRate.Storage.Repository;

namespace TeacherRate.Api;

public static class DependencyExtension
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        return services
            .AddMapperProfile()
            .AddServices();
    }

    private static IServiceCollection AddMapperProfile(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new TeacherRateProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IRepository, GenericRepository>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ITeacherService, TeacherService>();
        services.AddTransient<IAdminService, AdminService>();

        return services;
    }
}
