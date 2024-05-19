using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherRate.Api.DTOs;
using TeacherRate.Api.Models.Requests;
using TeacherRate.Api.Models.Responses;
using TeacherRate.Api.Services;
using TeacherRate.Domain.Interfaces;

namespace TeacherRate.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AuthController(IMapper mapper, IAuthService authService)
    {
        _mapper = mapper;
        _authService = authService;
    }

    [HttpGet("me"), Authorize]
    public async Task<ActionResult<UserDTO>> GetUserInfo()
    {
        var id = HttpContext?.Session.GetInt32("UserId");
        if (!id.HasValue)
        {
            return Unauthorized();
        }

        var user = await _authService.GetMe(id!.Value);
        return Ok(_mapper.Map<UserDTO>(user));
    }

    //admin: admin@admin.com pass: admin

    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login([FromBody] LoginRequest request)
    {
        try
        {
            var authData = await _authService.Login(request.Email, request.Password);

            HttpContext?.Session.SetInt32("UserId", authData.UserId);

            return Ok(new LoginResponse()
            {
                Token = authData.Token,
                Role = authData.Role,
            });
        }
        catch(ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("createAdmin")]
    public async Task<ActionResult<UserDTO>> CreateAdmin(CreateUserRequest request)
    {
        var user = await (_authService as AuthService).CreateAdmin(request);
        return Created("", _mapper.Map<UserDTO>(user));
    }
}
