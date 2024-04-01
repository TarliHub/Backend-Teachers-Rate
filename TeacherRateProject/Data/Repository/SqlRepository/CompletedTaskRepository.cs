using TeacherRateProject.Data.Repository.Implementation;
using TeacherRateProject.Data.Repository.Interfaces;
using TeacherRateProject.Models;

namespace TeacherRateProject.Data.Repository.SqlRepository;

public class CompletedTaskRepository : GenericRepository<CompletedTask>,
    ICompletedTaskRepository
{
    public CompletedTaskRepository(DataContext context) : base(context)
    {
        
    }
}
