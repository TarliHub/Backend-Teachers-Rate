using System.Text.Json.Serialization;

namespace TeacherRate.Api.DTOs;

public class HeadTeacherDTO : UserDTO
{
    public HeadTeacherDTO()
    {
        Teachers = new();
    }

    [JsonIgnore]
    public List<TeacherDTO> Teachers { get; set; }
}
