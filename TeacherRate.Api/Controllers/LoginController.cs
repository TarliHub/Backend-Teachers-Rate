using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
}
