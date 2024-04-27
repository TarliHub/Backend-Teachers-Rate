﻿using TeacherRate.Domain.Models;

namespace TeacherRate.Api.DTOs;

public class UserTaskDTO
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? PointsDescription { get; set; }
    public required string Approval { get; set; }
    public TaskCategoryDTO Category { get; set; } = null!;
}
