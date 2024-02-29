using Microsoft.AspNetCore.Mvc;
using TeacherRateProject.Data.Repository.Interfaces;
using TeacherRateProject.Models;

namespace TeacherRateProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository<User, int> _userRepository;

        public UserController(IUserRepository<User, int> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            var addedUser = await _userRepository.AddUser(user);

            return Ok(addedUser);
        }
    }
}
