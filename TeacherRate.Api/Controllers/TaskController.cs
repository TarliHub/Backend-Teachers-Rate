using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TeacherRate.Api.DTOs;
using TeacherRate.Api.Models.Paging;
using TeacherRate.Api.Models.Requests;
using TeacherRate.Domain.Interfaces;
using TeacherRate.Domain.Models;
namespace TeacherRate.Api.Controllers;

[Route("api/tasks")]
[Authorize]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TaskController(ITaskService taskService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _taskService = taskService;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
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
        var identifier = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (identifier == null)
            return Unauthorized("token does not contain id");

        int id = int.Parse(identifier);

        var tasks = await _taskService.GetUserTasks<TeacherBase>(id);

        var page = tasks.ToPagedList(pageRequest.Page, pageRequest.Size);

        return Ok(page.Map<CompletedTaskDTO>(_mapper));
    }

    [HttpGet("completed-tasks/{id}")]
    public async Task<ActionResult<PagedList<CompletedTaskDTO>>> GetCompletedUserTasksById(
        int id, [FromQuery] PageRequest pageRequest)
    {
        var tasks = await _taskService.GetUserTasks<TeacherBase>(id);

        var page = tasks.ToPagedList(pageRequest.Page, pageRequest.Size);

        return Ok(page.Map<UserTaskDTO>(_mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserTaskDTO>> GetUserTaskById(int id)
    {
        var task = await _taskService.GetTaskById(id);

        return Ok(_mapper.Map<UserTaskDTO>(task));
    }

    [HttpPost("send-request")]
    public async Task<ActionResult<bool>> SendRequest(TeacherRequestDTO requestDTO)
    {
        var identifier = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (identifier == null)
            return Unauthorized("token does not contain id");

        int id = int.Parse(identifier);

        var request = _mapper.Map<TeacherRequest>(requestDTO);

        return Ok(await _taskService.SendTask(request, id));
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

    [HttpDelete("deleteAll"), Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteAllCompletedTasks()
    {
        await _taskService.DeleteAllCompletedTasks();
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserTaskDTO>> UpdateTask(int id, CreateTaskRequest request)
    {
        var task = await _taskService.GetTaskById(id);
        if(task == null)
            return NotFound($"Task with id {id} not found.");

        task.Approval = request.Approval;
        task.Title = request.Title;
        task.Points = request.Points;
        task.PointsDescription = request.PointsDescription;
        
        var taskFromDb = await _taskService.UpdateTask(task);
        return _mapper.Map<UserTaskDTO>(taskFromDb);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTask(int id)
    {
        await _taskService.RemoveTask(id);
        return NoContent();
    }
}
