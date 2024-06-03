using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeacherRate.Api.DTOs;
using TeacherRate.Api.Models.Requests;
using TeacherRate.Domain.Interfaces;
using TeacherRate.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using TeacherRate.Api.Models.Paging;
using System.Security.Claims;

namespace TeacherRate.Api.Controllers;

[Route("api/teachers")]
[Authorize(Roles = "HeadTeacher, Admin")]
[ApiController]
public class TeacherController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TeacherController(IUserService userService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _userService = userService;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet()]
    public async Task<ActionResult<PagedList<TeacherWithHeadTeacherDTO>>> GetTeachers(
        [FromQuery] PageRequest pageRequest)
    {
        var id = TryGetIdFromToken();

        var users = _userService.GetTeachers(id);

        var page = users.ToPagedList(pageRequest.Page, pageRequest.Size);

        return Ok(page.Map<TeacherWithHeadTeacherDTO>(_mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TeacherWithHeadTeacherDTO>> GetTeacherById(int id)
    {
        var user = await _userService.GetUserById<Teacher>(id);

        if (user is null)
            return NotFound("Teacher not found");

        return Ok(_mapper.Map<TeacherWithHeadTeacherDTO>(user));
    }

    [HttpPost]
    public async Task<ActionResult<TeacherWithHeadTeacherDTO>> AddTeacher(CreateUserRequest request)
    {
        var id = TryGetIdFromToken();

        var headTeacher = await _userService.GetUserById<HeadTeacher>(id);
        if (headTeacher == null)
            return NotFound("HeadTeacher not found");

        var user = new Teacher()
        {
            Name = request.Name,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Email = request.Email,
            CreatedAt = DateTime.UtcNow,
            HeadTeacher = headTeacher,
        };

        var userFromDb = await _userService.AddUser(user, request.Password);

        return Created(string.Empty, _mapper.Map<TeacherWithHeadTeacherDTO>(userFromDb));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TeacherDTO>> UpdateTeacher(int id, CreateUserRequest request)
    {
        var user = await _userService.GetUserById<Teacher>(id);
        if (user == null)
            return NotFound($"user with id {id} not found.");

        user.Name = request.Name;
        user.LastName = request.LastName;
        user.MiddleName = request.MiddleName;
        user.Email = request.Email;
        var userFromDb = await _userService.UpdateUser(user, request.Password);

        return Ok(_mapper.Map<TeacherDTO>(userFromDb));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        await _userService.RemoveUser(id);

        return NoContent();
    }

    private int TryGetIdFromToken()
    {
        var identifier = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (identifier == null)
            throw new UnauthorizedAccessException("token does not contain id");

        return int.Parse(identifier);
    }
}
