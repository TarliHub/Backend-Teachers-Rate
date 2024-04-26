using Microsoft.AspNetCore.Mvc;
using TeacherRate.Domain.Models;

namespace TeacherRate.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return Ok(new List<User>
        {
            new()
            {
                Id = 1,
                Name = "Test",
                LastName = "Test",
                Email = "Test",
            }
        });
    }
}
