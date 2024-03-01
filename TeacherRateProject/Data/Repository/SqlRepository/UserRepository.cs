using Microsoft.EntityFrameworkCore;
using TeacherRateProject.Data.Repository.Implementation;
using TeacherRateProject.Data.Repository.Interfaces;
using TeacherRateProject.Models;

namespace TeacherRateProject.Data.Repository.SqlRepository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(DataContext context) : base(context)
    {
    }
}
