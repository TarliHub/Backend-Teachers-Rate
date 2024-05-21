using TeacherRate.Domain.Models;

namespace TeacherRate.Domain.Interfaces;

public interface ICategoryService
{
    Task<TaskCategory> AddCategory(TaskCategory category);
    IQueryable<TaskCategory> GetCategories();
    Task<TaskCategory?> GetCategoryById(int id);
}
