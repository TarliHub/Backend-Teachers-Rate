using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherRate.Api.DTOs;
using TeacherRate.Api.Models.Paging;
using TeacherRate.Api.Models.Requests;
using TeacherRate.Domain.Interfaces;
using TeacherRate.Domain.Models;

namespace TeacherRate.Api.Controllers;

[Route("api/head-teachers")]
[Authorize(Roles = "Admin")]
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
    public async Task<ActionResult<PagedList<HeadTeacherWithTeachersDTO>>> GetHeadTeachers(
        [FromQuery] PageRequest pageRequest)
    {
        var users = _userService.GetHeadTeachers();

        var page = users.ToPagedList(pageRequest.Page, pageRequest.Size);

        return Ok(page.Map<HeadTeacherWithTeachersDTO>(_mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HeadTeacherWithTeachersDTO>> GetHeadTeacherById(int id)
    {
        var user = await _userService.GetUserById<HeadTeacher>(id);

        if (user is null)
            return NotFound("Head teacher not found");

        return Ok(_mapper.Map<HeadTeacherWithTeachersDTO>(user));
    }

    [HttpPost]
    public async Task<ActionResult<HeadTeacherDTO>> AddHeadTeacher(CreateUserRequest request)
    {
        if (request.CommissionName == null)
            return BadRequest("Commission name must be specified");

        var user = new HeadTeacher() {
            Name = request.Name,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Email = request.Email,
            CreatedAt = DateTime.UtcNow,
            CommissionName = request.CommissionName,
        };

        var userFromDb = await _userService.AddUser(user, request.Password);

        return Created(string.Empty, _mapper.Map<HeadTeacherDTO>(userFromDb));
    }

    [HttpPut]
    public async Task<ActionResult<HeadTeacherDTO>> UpdateHeadTeacher(HeadTeacherDTO user)
    {
        var userFromDb = await _userService.UpdateUser(_mapper.Map<HeadTeacher>(user));

        return Ok(_mapper.Map<HeadTeacherDTO>(userFromDb));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteHeadTeacher(int id)
    {
        await _userService.RemoveUser(id);

        return NoContent();
    }
}
