using TeacherRate.Domain.Interfaces.Base;
using TeacherRate.Domain.Models;

namespace TeacherRate.Domain.Interfaces;

public interface IAdminService : IUserService
{
    Task<UserTask> AddTask(UserTask task);
    Task<bool> RemoveTask(int id);
    Task<UserTask> UpdateTask(int id, UserTask task);
    Task<List<CompletedTask>> GetHeadTeacherTasks(int id, int index, int size);

    Task<TaskCategory> AddCategory(TaskCategory category);
    Task<bool> RemoveCategory(int id);
    Task<TaskCategory> UpdateCategory(int id, TaskCategory category);

    Task<HeadTeacher> AddHeadTeacher(HeadTeacher headTeacher, string password);
    Task<bool> RemoveHeadTeacher(int id);
    Task<HeadTeacher> UpdateHeadTeacher(int id, HeadTeacher headTeacher);
    Task<List<HeadTeacher>> GetHeadTeachers(int index, int size);
    Task<HeadTeacher?> GetHeadTeacherById(int id);
}
