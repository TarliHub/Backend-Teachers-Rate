using TeacherRate.Domain.Interfaces.Base;
using TeacherRate.Domain.Models;

namespace TeacherRate.Domain.Interfaces;

public interface ITeacherService : IUserService
{
    Task<bool> SendTask(TeacherRequest request);
    Task<List<CompletedTask>> GetCompletedTasks(int index, int size);
}
