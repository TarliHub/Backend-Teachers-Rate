using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeacherRateProject.Data.UnitOfWork;
using TeacherRateProject.DTOs;
using TeacherRateProject.Models;

namespace TeacherRateProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            var users = await _unitOfWork.User.GetAll();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _unitOfWork.User.GetById(id);

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser(UserDto user)
        {
            var addedUser = await _unitOfWork.User.Add(_mapper.Map<User>(user));
            _unitOfWork.Save();

            return Ok(addedUser);
        }
    }
}
