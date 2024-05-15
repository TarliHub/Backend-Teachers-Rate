using System.Text.Json.Serialization;

namespace TeacherRate.Api.DTOs;

public class HeadTeacherDTO : TeacherBaseDTO
{
    public required string CommissionName { get; set; }
}
