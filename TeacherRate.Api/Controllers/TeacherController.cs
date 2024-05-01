using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using TeacherRate.Api.DTOs;
using TeacherRate.Api.Models;
using TeacherRate.Domain.Interfaces;
using TeacherRate.Domain.Models;

namespace TeacherRate.Api.Controllers;

[Route("api/teacher")]
[ApiController]
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _teacherService;
    private readonly IMapper _mapper;

    public TeacherController(ITeacherService taskService, IMapper mapper)
    {
        _teacherService = taskService;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetTeacher(int id)
    {
        return Ok(await _teacherService.GetUserById(id));
    }

    [HttpGet("tasks")]
    public async Task<ActionResult<PageModel<UserTaskDTO>>> GetTasks(
        [FromQuery] PageRequest pageRequest)
    {
        var tasks = await _teacherService.GetTasks(0, 10);

        var page = new PageModel<UserTaskDTO>(pageRequest)
        {
            Items = _mapper.Map<List<UserTaskDTO>>(tasks),
        };

        return Ok(page);
    }

    [HttpGet("completedTasks")]
    public async Task<ActionResult<PageModel<CompletedTaskDTO>>> GetCompletedTasks(
        [FromQuery] PageRequest pageRequest)
    {
        var tasks = await _teacherService.GetCompletedTasks(0, 10);

        var page = new PageModel<CompletedTaskDTO>(pageRequest)
        {
            Items = _mapper.Map<List<CompletedTaskDTO>>(tasks)
        };

        return Ok(page);
    }

    [HttpGet("tasks/{id}")]
    public async Task<ActionResult<UserTaskDTO>> GetTask(int id)
    {
        var task = await _teacherService.GetTaskById(id);

        return Ok(_mapper.Map<UserTaskDTO>(task));
    }

    [HttpPost("tasks")]
    public async Task<ActionResult> SendTask(TeacherRequestDTO request)
    {
        var isRequestSend = await _teacherService.SendTask(_mapper.Map<TeacherRequest>(request));

        if (!isRequestSend) 
            return BadRequest();

        return Ok();
    }
}
