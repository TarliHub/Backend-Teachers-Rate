using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeacherRate.Api.DTOs;
using TeacherRate.Api.Models.Paging;
using TeacherRate.Api.Models.Requests;
using TeacherRate.Domain.Interfaces;
using TeacherRate.Domain.Models;
namespace TeacherRate.Api.Controllers;

[Route("api/tasks")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly IMapper _mapper;

    public TaskController(ITaskService taskService, IMapper mapper)
    {
        _taskService = taskService;
        _mapper = mapper;
    }

    [HttpGet()]
    public async Task<ActionResult<PagedList<UserTaskDTO>>> GetUserTasks(
        [FromQuery] PageRequest pageRequest)
    {
        var tasks = _taskService.GetTasks();
        var page = tasks.ToPagedList(pageRequest.Page, pageRequest.Size);

        return Ok(page.Map<UserTaskDTO>(_mapper));
    }

    [HttpGet("completed-tasks")]
    public async Task<ActionResult<PagedList<CompletedTaskDTO>>> GetCompletedUserTasks(
        [FromQuery] PageRequest pageRequest)
    {
        var id = HttpContext.Session.GetInt32("UserId");
        if (!id.HasValue)
            return Unauthorized();

        var tasks = await _taskService.GetUserTasks<TeacherBase>(id.Value);

        var page = tasks.ToPagedList(pageRequest.Page, pageRequest.Size);

        return Ok(page.Map<UserTaskDTO>(_mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserTaskDTO>> GetUserTaskById(int id)
    {
        var task = await _taskService.GetTaskById(id);

        return Ok(_mapper.Map<UserTaskDTO>(task));
    }

    [HttpPost]
    public async Task<ActionResult<UserTaskDTO>> AddUserTask(UserTaskDTO userTaskDTO)
    {
        var task = await _taskService.AddTask(_mapper.Map<UserTask>(userTaskDTO));

        return Ok(_mapper.Map<UserTaskDTO>(task));
    }


}
