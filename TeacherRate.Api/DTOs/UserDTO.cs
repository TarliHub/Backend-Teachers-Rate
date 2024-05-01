namespace TeacherRate.Api.DTOs;

public class UserDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required string MiddleName { get; set; }
    public required string Email { get; set; }
    public DateTime CreatedAt { get; set; }
}
