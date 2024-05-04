using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeacherRate.Api.DTOs;
using TeacherRate.Api.Models;
using TeacherRate.Api.Models.Requests;
using TeacherRate.Domain.Interfaces;
using TeacherRate.Domain.Models;

namespace TeacherRate.Api.Controllers;

[Route("api/head-teachers")]
[ApiController]
public class HeadTeacherController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public HeadTeacherController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet()]
    public async Task<ActionResult<PageModel<HeadTeacherDTO>>> GetHeadTeachers(
        [FromQuery] PageRequest pageRequest)
    {
        var users = await _userService.GetHeadTeachers(pageRequest.Page, pageRequest.Size);

        var page = new PageModel<HeadTeacherDTO>(pageRequest)
        {
            Items = _mapper.Map<List<HeadTeacherDTO>>(users)
        };

        return Ok(page);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HeadTeacherDTO>> GetHeadTeacherById(int id)
    {
        var user = await _userService.GetUserById<HeadTeacher>(id);

        if (user is null)
            return NotFound("Head teacher not found");

        return Ok(_mapper.Map<HeadTeacherDTO>(user));
    }

    [HttpPost]
    public async Task<ActionResult<HeadTeacherDTO>> AddHeadTeacher(CreateUserRequest request)
    {
        var user = new HeadTeacher() {
            Name = request.Name,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Email = request.Email,
            CreatedAt = DateTime.UtcNow,
        };

        var userFromDb = await _userService.AddUser(user, request.Password);

        return Created(string.Empty, _mapper.Map<HeadTeacherDTO>(userFromDb));
    }

    [HttpPut]
    public async Task<ActionResult<HeadTeacherDTO>> UpdateUser(HeadTeacherDTO user)
    {
        var userFromDb = await _userService.UpdateUser(_mapper.Map<HeadTeacher>(user));

        return Ok(_mapper.Map<HeadTeacherDTO>(userFromDb));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        await _userService.RemoveUser(id);

        return NoContent();
    }
}
