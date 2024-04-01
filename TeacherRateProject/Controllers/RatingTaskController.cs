using Microsoft.AspNetCore.Mvc;
using TeacherRateProject.DTOs;
using TeacherRateProject.Models;
using TeacherRateProject.Services.Interfaces;

namespace TeacherRateProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RatingTaskController : ControllerBase
{
    private readonly IRatingTaskService<RatingTaskDto, int> _ratingTaskService;

    public RatingTaskController(IRatingTaskService<RatingTaskDto, int> ratingTaskService)
    {
        _ratingTaskService = ratingTaskService;
    }

    [HttpGet]
    public async Task<ActionResult<List<RatingTaskDto>>> GetAllTasks()
    {
        var tasks = await _ratingTaskService.GetAll();

        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RatingTask>> GetTaskById(int id)
    {
        var task = await _ratingTaskService.GetById(id);

        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<RatingTaskDto>> AddTask(RatingTaskDto task)
    {
        var addedTask = await _ratingTaskService.Add(task);

        return Ok(addedTask);
    }
}
