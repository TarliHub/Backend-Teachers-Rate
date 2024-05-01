using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeacherRate.Api.DTOs;
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
    public async Task<ActionResult<IEnumerable<UserTaskDTO>>> GetTasks()
    {
        var tasks = await _adminService.GetTasks(0, 10);

        return Ok(_mapper.Map<List<UserTaskDTO>>(tasks));
    } 
}
