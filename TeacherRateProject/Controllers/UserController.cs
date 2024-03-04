using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeacherRateProject.Data.UnitOfWork;
using TeacherRateProject.DTOs;
using TeacherRateProject.Models;
using TeacherRateProject.Services.Interfaces;

namespace TeacherRateProject.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost]
        public async Task<ActionResult<User>> AddUser(UserDto user)
        {
            var addedUser = await _userService.Add(user);

            return Ok(addedUser);
        }
    }
}
