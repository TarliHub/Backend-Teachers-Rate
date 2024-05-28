using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TeacherRate.Api.DTOs;
using TeacherRate.Api.Models.Requests;
using TeacherRate.Api.Models.Responses;
using TeacherRate.Api.Services;
using TeacherRate.Domain.Interfaces;
using TeacherRate.Domain.Models;

namespace TeacherRate.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthController(IMapper mapper, IAuthService authService, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _authService = authService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet("rate"), Authorize]
    public async Task<ActionResult<TeacherBaseDTO>> GetRateInfo()
    {
        var id = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (id == null)
            return Unauthorized("token does not contain id");

        var user = await _authService.GetMe(int.Parse(id));
        return Ok(_mapper.Map<TeacherBaseDTO>(user as TeacherBase));
    }

    [HttpGet("me"), Authorize]
    public async Task<ActionResult<UserDTO>> GetUserInfo()
    {
        var id = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (id == null)
            return Unauthorized("token does not contain id");

        var user = await _authService.GetMe(int.Parse(id));
        return Ok(_mapper.Map<UserDTO>(user));
    }

    //admin: a@a.com pass: a

    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login([FromBody] LoginRequest request)
    {
        try
        {
            var authData = await _authService.Login(request.Email, request.Password);

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
