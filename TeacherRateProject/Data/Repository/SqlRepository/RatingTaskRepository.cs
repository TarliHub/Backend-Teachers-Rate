using TeacherRateProject.Data.Repository.Implementation;
using TeacherRateProject.Data.Repository.Interfaces;
using TeacherRateProject.Models;

namespace TeacherRateProject.Data.Repository.SqlRepository;

public class RatingTaskRepository : GenericRepository<RatingTask>,
    IRatingTaskRepository
{
    public RatingTaskRepository(DataContext context) : base(context)
    {
        
    }
}
