using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeacherRate.Api.DTOs;
using TeacherRate.Api.Models;
using TeacherRate.Api.Models.Requests;
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
        var tasks = await _adminService.GetTasks(0, 10);

        var page = new PageModel<UserTaskDTO>(pageRequest)
        {
            Items = _mapper.Map<List<UserTaskDTO>>(tasks)
        };

        return Ok(page);
    }
}
