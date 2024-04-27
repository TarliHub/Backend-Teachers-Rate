using Microsoft.AspNetCore.Mvc;
using TeacherRate.Domain.Models;

namespace TeacherRate.Api.Controllers;

[Route("api/teacher")]
[ApiController]
public class TeacherController : ControllerBase
{
    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    //{
    //    return Ok(new List<User>
    //    {
    //        new()
    //        {
    //            Id = 1,
    //            Name = "Test",
    //            LastName = "Test",
    //            Email = "Test",
    //        }
    //    });
    //}

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetTeacher(int id)
    {
        return Ok(new User()
        {
            Id = 1,
            Name = "Name",
            LastName = "LastName",
            Email = "test@test.com"
        });
    }

    [HttpGet("tasks")]
    public async Task<ActionResult<IEnumerable<UserTask>>> GetTasks()
    {
        var tasks = new List<UserTask>
        {
            new()
            {
                Id = 1,
                Title = "Test Task",
            }
        };

        return Ok(tasks);
    }

    [HttpGet("tasks/{id}")]
    public async Task<ActionResult<UserTask>> GetTask(int id)
    {
        var task = new UserTask()
        {
            Id = 1,
            Title = "Test Task",
        };

        return Ok(task);
    }

    [HttpPost("tasks")]
    public async Task<ActionResult> SendTask([FromBody] UserTask task)
    {
        return Ok();
    }
}
