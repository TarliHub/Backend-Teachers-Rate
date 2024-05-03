using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeacherRate.Api.DTOs;
using TeacherRate.Api.Models;
using TeacherRate.Domain.Interfaces;

namespace TeacherRate.Api.Controllers;

[Route("api/admin")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;
    private readonly IMapper _mapper;

    public AdminController(IAdminService adminService, IMapper mapper)
    {
        _adminService = adminService;
        _mapper = mapper;
    }

    [HttpGet("tasks")]
    public async Task<ActionResult<PageModel<UserTaskDTO>>> GetTasks(
        [FromQuery] PageRequest pageRequest)
    {
        var tasks = await _adminService.GetTasks(pageRequest.Page, pageRequest.Size);

        var page = new PageModel<UserTaskDTO>(pageRequest)
        {
            Items = _mapper.Map<List<UserTaskDTO>>(tasks)
        };

        return Ok(page);
    }

    [HttpGet("tasks/{id}")]
    public async Task<ActionResult<PageModel<CompletedTaskDTO>>> GetHeadTeacherTasks(
        int id, [FromQuery] PageRequest pageRequest)
    {
        var tasks = await _adminService.GetHeadTeacherTasks(id, pageRequest.Page, pageRequest.Size);

        var page = new PageModel<CompletedTaskDTO>(pageRequest)
        {
            Items = _mapper.Map<List<CompletedTaskDTO>>(tasks),
        };

        return Ok(page);
    }

    [HttpGet("me")]
    public async Task<ActionResult<UserDTO>> GetUserInfo()
    {
        var id = HttpContext.Session.GetInt32("UserId");
        if (!id.HasValue)
            return Unauthorized();

        var user = await _adminService.GetUserById(id.Value);
        return Ok(_mapper.Map<UserDTO>(user));
    }

}
