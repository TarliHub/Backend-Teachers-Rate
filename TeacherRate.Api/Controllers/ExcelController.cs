using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using TeacherRate.Domain.Interfaces;
using TeacherRate.Domain.Models;

namespace TeacherRate.Api.Controllers;

[Route("api/excel")]
[Authorize(Roles = "Admin")]
[ApiController]
public class ExcelController : ControllerBase
{
    private readonly IUserService _userService;

    public ExcelController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("rates")]
    public async Task<ActionResult> GetRatesExcelFile()
    {
        var headTeachers = await _userService.GetHeadTeachers().ToListAsync();
        var users = new List<TeacherBase>();
        foreach (var headTeacher in headTeachers)
        {
            users.AddRange(await _userService.GetTeachers(headTeacher.Id).ToListAsync());
        }
        users.AddRange(headTeachers);

        var workbook = CreateExcelFile(users);

        using var stream = new MemoryStream();
        workbook.Write(stream);
        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"rating{DateTime.Now.ToString("yyyy-MM-dd")}.xlsx");
    }

    private IWorkbook CreateExcelFile(List<TeacherBase> teachers)
    {
        teachers = teachers.OrderByDescending(x => x.Points).ToList();

        var workbook = new XSSFWorkbook();
        var sheet = workbook.CreateSheet("Sheet1");
        var firstRow = sheet.CreateRow(0);

        firstRow.CreateCell(0).SetCellValue("Ім'я");
        firstRow.CreateCell(1).SetCellValue("Рейтинг");

        for(int i = 0; i < teachers.Count; i++)
        {
            var row = sheet.CreateRow(i + 1);
            row.CreateCell(0).SetCellValue($"{teachers[i].LastName} {teachers[i].Name} {teachers[i].MiddleName}");
            row.CreateCell(1).SetCellValue(teachers[i].Points);
        }

        return workbook;
    }
}
