using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeacherRate.Api.DTOs;
using TeacherRate.Api.Models.Requests;
using TeacherRate.Api.Models;
using TeacherRate.Domain.Interfaces;
using TeacherRate.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using TeacherRate.Api.Models.Paging;

namespace TeacherRate.Api.Controllers;

[Route("api/teachers")]
[Authorize(Roles = "HeadTeacher")]
[ApiController]
public class TeacherController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public TeacherController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet()]
    public async Task<ActionResult<PagedList<TeacherDTO>>> GetTeachers(
        [FromQuery] PageRequest pageRequest)
    {
        var id = HttpContext.Session.GetInt32("UserId");
        if (!id.HasValue)
            id = 2;
        //return Unauthorized();

        var users = _userService.GetTeachers(id!.Value);

        var page = users.ToPagedList(pageRequest.Page, pageRequest.Size);

        return Ok(page.Map<TeacherDTO>(_mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TeacherDTO>> GetTeacherById(int id)
    {
        var user = await _userService.GetUserById<Teacher>(id);

        if (user is null)
            return NotFound("Teacher not found");

        return Ok(_mapper.Map<TeacherDTO>(user));
    }

    [HttpPost]
    public async Task<ActionResult<TeacherDTO>> AddTeacher(CreateUserRequest request)
    {
        var user = new Teacher()
        {
            Name = request.Name,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Email = request.Email,
            CreatedAt = DateTime.UtcNow,
        };

        var userFromDb = await _userService.AddUser(user, request.Password);

        return Created(string.Empty, _mapper.Map<TeacherDTO>(userFromDb));
    }

    [HttpPut]
    public async Task<ActionResult<TeacherDTO>> UpdateTeacher(TeacherDTO user)
    {
        var userFromDb = await _userService.UpdateUser(_mapper.Map<Teacher>(user));

        return Ok(_mapper.Map<TeacherDTO>(userFromDb));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        await _userService.RemoveUser(id);

        return NoContent();
    }
}
