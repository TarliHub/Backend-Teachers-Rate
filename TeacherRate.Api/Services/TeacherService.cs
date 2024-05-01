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

    public async Task<IEnumerable<CompletedTask>> GetCompletedTasks(int index, int size)
    {
        return await _repository.GetAll<CompletedTask>(index, size);
    }

    public async Task<bool> SendTask(TeacherRequest request)
    {
        return await _repository.Add(request) != null;
    }
}
