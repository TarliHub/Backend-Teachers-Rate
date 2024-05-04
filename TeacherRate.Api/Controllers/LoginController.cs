using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherRate.Api.DTOs;
using TeacherRate.Api.Models.Requests;
using TeacherRate.Domain.Models;
using TeacherRate.Helpers;
using TeacherRate.Storage.Abstraction.Interfaces;

namespace TeacherRate.Api.Controllers;

[Route("api/login")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public LoginController(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("me")]
    public async Task<ActionResult<UserDTO>> GetUserInfo()
    {
        var id = HttpContext.Session.GetInt32("UserId");
        if (!id.HasValue)
            id = 1;
        //return Unauthorized();

        var user = await _repository.GetById<User>(id.Value);
        return Ok(_mapper.Map<UserDTO>(user));
    }

    //admin: admin@admin.com pass: admin

    [HttpPost]
    public async Task<ActionResult<UserDTO>> Login(LoginRequest loginRequest)
    {
        string passwordHash = PasswordManager.ComputeHashPassword(loginRequest.Password);
        var user = await _repository.QueryOne<User>(user => user.Email == loginRequest.Email);
        var credentials = await _repository.QueryOne<CredentialsInfo>(creds => creds.PasswordHash == passwordHash);
        if (user is null || credentials is null)
            return BadRequest();

        if (user.Id != credentials.UserId)
            return BadRequest();

        return Ok(_mapper.Map<UserDTO>(user));
    }

    [HttpPost("createAdmin")]
    public async Task<ActionResult<UserDTO>> CreateAdmin(CreateUserRequest request)
    {
        var userFromDb = await _repository.QueryOne<User>(u => u.Role == Role.Admin);
        if (userFromDb != null)
            return BadRequest("Admin aready exists");

        var user = new User()
        {
            Name = request.Name,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Email = request.Email,
            CreatedAt = DateTime.UtcNow,
        };

        var entity = _repository.Add(user);
        var credentials = new CredentialsInfo()
        {
            UserId = entity.Id,
            PasswordHash = PasswordManager.ComputeHashPassword(request.Password)
        };
        await _repository.SaveChanges();
        return Created("", _mapper.Map<UserDTO>(user));
    }
}
