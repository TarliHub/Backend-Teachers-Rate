using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeacherRate.Api.Models.Requests;
using TeacherRate.Domain.Exceptions;
using TeacherRate.Domain.Interfaces;
using TeacherRate.Domain.Models;
using TeacherRate.Helpers;
using TeacherRate.Storage.Abstraction.Interfaces;

namespace TeacherRate.Api.Services;

public class AuthService : IAuthService
{
    private readonly IRepository _repository;
    private readonly IConfiguration _configuration;

    public AuthService(IRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    public async Task<User> GetMe(int id)
    {
        var user = await _repository.GetById<User>(id);
        if (user is null)
            throw new DetailedException("User not found", nameof(user));

        return user;
    }

    public async Task<AuthData> Login(string email, string password)
    {
        string passwordHash = PasswordManager.ComputeHashPassword(password);
        var user = await _repository.QueryOne<User>(t => t.Email == email);
        if (user is null)
            throw new DetailedException($"User with email {email} not found", nameof(email));

        var credentials = await _repository.QueryOne<CredentialsInfo>(creds => creds.User.Id == user.Id);
        if (credentials is null || passwordHash != credentials.PasswordHash)
            throw new DetailedException($"Password did not match the user's password", nameof(password));

        var token = GenerateToken(user);
        return new AuthData()
        {
            Token = token,
            Role = user.Role,
            UserId = user.Id,
        };
    }

    private string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                int.Parse(_configuration.GetSection("AppSettings:ExpireTime").Value!)),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    public async Task<User> CreateAdmin(CreateUserRequest request)
    {
        var userFromDb = await _repository.QueryOne<User>(u => u.Role == Role.Admin);
        if (userFromDb != null)
            throw new DetailedException("admin already exists", "admin");

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
            User = entity,
            PasswordHash = PasswordManager.ComputeHashPassword(request.Password)
        };
        _repository.Add(credentials);
        await _repository.SaveChanges();

        return entity;
    }
}
