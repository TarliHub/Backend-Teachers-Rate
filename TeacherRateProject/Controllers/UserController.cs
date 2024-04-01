using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeacherRateProject.DTOs;
using TeacherRateProject.Models;
using TeacherRateProject.Services.Interfaces;

namespace TeacherRateProject.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService<UserDto, int> _userService;

        public UserController(IUserService<UserDto, int> userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAll();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetById(id);

            return Ok(user);
        }

        [HttpGet("{id}/tasks")]
        public async Task<ActionResult<IEnumerable<CompletedTaskDto>>> GetCompletedTasks(int id){
            var tasks = await _userService.GetUserTasks(id);

            return Ok(tasks);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetPagesUsers(int pageIndex, int pageSize)
        {
            var users = await _userService.GetPaged(pageIndex, pageSize);

            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser(UserDto user)
        {
            var addedUser = await _userService.Add(user);

            return Ok(addedUser);
        }

        [HttpPost("{id}/tasks")]
        public async Task<ActionResult<CompletedTaskDto>> AddCompletedTasks(int id, PostCompletedTaskDto postTask)
        {
            var user = await _userService.GetById(id);

            var task = await _userService.AddTaskToUser(id, postTask);

            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userService.Delete(id);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, UserDto user)
        {
            await _userService.Update(id, user);

            return Ok();
        }
    }
}
