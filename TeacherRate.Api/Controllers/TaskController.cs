using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeacherRate.Domain.Interfaces;
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

}
