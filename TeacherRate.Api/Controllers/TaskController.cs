using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<ActionResult<UserTaskDTO>> AddUserTask(CreateTaskRequest request)
    {
        var task = new UserTask()
        {
            Approval = request.Approval,
            Title = request.Title,
            Points = request.Points,
            PointsDescription = request.PointsDescription
        };
        var taskFromDb = await _taskService.AddTask(task, request.CategoryId);

        return Ok(_mapper.Map<UserTaskDTO>(taskFromDb));
    }


}
