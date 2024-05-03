using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeacherRate.Api.DTOs;
using TeacherRate.Api.Models;
using TeacherRate.Api.Models.Requests;
using TeacherRate.Domain.Interfaces;
using TeacherRate.Domain.Models;

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
    public async Task<ActionResult<PageModel<UserTask>>> GetTasks(
        [FromQuery] PageRequest pageRequest)
    {
        var tasks = await _adminService.GetTasks(pageRequest.Page, pageRequest.Size);

        var page = new PageModel<UserTask>(pageRequest)
        {
            Items = tasks
        };

        return Ok(page);
    }

    [HttpPost("tasks")]
    public async Task<ActionResult<UserTaskDTO>> CreateTask(CreateTaskRequest request)
    {
        var category = await _adminService.GetCategoryById(request.CategoryId);

        if (category is null)
            return NotFound("category not found");

        var task = new UserTask()
        {
            Title = request.Title,
            Approval = request.Approval,
            Points = new(request.Points),
            PointsDescription = request.PointsDescription,
            Category = category,
        };

        var taskFromDb = await _adminService.AddTask(task);
        return Ok(_mapper.Map<UserTaskDTO>(taskFromDb));
    }

    [HttpGet("categories")]
    public async Task<ActionResult<IEnumerable<TaskCategoryDTO>>> GetCategories()
    {
        var categories = await _adminService.GetCategories();
        return Ok(_mapper.Map<List<TaskCategoryDTO>>(categories));
    }

    [HttpPost("categories")]
    public async Task<ActionResult<TaskCategoryDTO>> CreateCategory(CreateCategoryRequest request)
    {
        var category = new TaskCategory()
        {
            Name = request.Name,
        };

        var categoryFromDb = await _adminService.AddCategory(category);
        return Ok(_mapper.Map<TaskCategoryDTO>(categoryFromDb));
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

    [HttpGet("head-teachers")]
    public async Task<ActionResult<PageModel<HeadTeacherDTO>>> GetHeadTeachers([FromQuery] PageRequest pageRequest)
    {
        var headTeachers = await _adminService.GetHeadTeachers(pageRequest.Page, pageRequest.Size);

        var page = new PageModel<HeadTeacherDTO>(pageRequest)
        {
            Items = _mapper.Map<List<HeadTeacherDTO>>(headTeachers)
        };

        return Ok(page);
    }

    [HttpGet("head-teachers/{id}/tasks")]
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

    [HttpPost("head-teachers")]
    public async Task<ActionResult<HeadTeacherDTO>> CreateHeadTeacher(CreateUserRequest request)
    {
        var headTeacher = new HeadTeacher()
        {
            Name = request.Name,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Email = request.Email,
            CreatedAt = DateTime.UtcNow,
        };

        var headTeacherFromDb = await _adminService.AddHeadTeacher(headTeacher, request.Password);

        return Ok(_mapper.Map<HeadTeacherDTO>(headTeacherFromDb));
    }


}
