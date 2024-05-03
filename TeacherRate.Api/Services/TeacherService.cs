using Microsoft.EntityFrameworkCore;
using TeacherRate.Api.Services.Base;
using TeacherRate.Domain.Interfaces;
using TeacherRate.Domain.Models;
using TeacherRate.Storage.Abstraction.Interfaces;

namespace TeacherRate.Api.Services;

public class TeacherService : UserService, ITeacherService
{
    private readonly IRepository _repository;

    public TeacherService(IRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public Task<List<CompletedTask>> GetCompletedTasks(int index, int size)
    {
        return _repository.GetAll<CompletedTask>(index, size).ToListAsync();
    }

    public async Task<bool> SendTask(TeacherRequest request)
    {
        throw new NotImplementedException();
    }
}
