using TeacherRate.Domain.Interfaces;
using TeacherRate.Domain.Models;
using TeacherRate.Storage.Abstraction.Interfaces;

namespace TeacherRate.Api.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepository _repository;

    public CategoryService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<TaskCategory> AddCategory(TaskCategory category)
    {
        if (string.IsNullOrEmpty(category.Name))
            throw new ArgumentException("Cannot create a category without a name", nameof(category));

        var categoryFromDb = _repository.Add(category);
        await _repository.SaveChanges();

        return categoryFromDb;
    }

    public IQueryable<TaskCategory> GetCategories()
    {
        return _repository.GetAll<TaskCategory>();
    }

    public Task<TaskCategory?> GetCategoryById(int id)
    {
        return _repository.GetById<TaskCategory>(id);
    }
}
