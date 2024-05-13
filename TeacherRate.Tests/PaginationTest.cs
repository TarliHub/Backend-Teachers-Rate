using TeacherRate.Api.Models;
using TeacherRate.Api.Models.Paging;
using TeacherRate.Api.Models.Requests;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TeacherRate.Tests;

public class PaginationTest
{
    [Theory]
    [InlineData(10)]
    [InlineData(0)]
    [InlineData(13)]
    [InlineData(999)]
    [InlineData(1)]
    public void PageModel_Count_CorrectValue(int count)
    {
        var data = new List<int>(Enumerable.Range(0, count)).AsQueryable();
        var pageRequest = new PageRequest()
        {
            Page = 0,
            Size = 5,
        };
        var page = data.ToPagedList(pageRequest.Page, pageRequest.Size);

        int actualCount = (int)Math.Ceiling((decimal)count / pageRequest.Size);

        Assert.Equal(actualCount, page.TotalPages);
    }

    [Fact]
    public void PageModel_Size_LessThanZero()
    {
        var data = new List<int>(Enumerable.Range(0, 10)).AsQueryable();

        var exceptionHandler = () =>
        {
            var pageRequest = new PageRequest()
            {
                Page = 0,
                Size = -1,
            };
            var page = data.ToPagedList(pageRequest.Page, pageRequest.Size);
        };

        Assert.Throws<ArgumentException>(exceptionHandler);
    }

    [Fact]
    public void PageModel_Page_LessThanZero()
    {
        var data = new List<int>(Enumerable.Range(0, 10)).AsQueryable();

        var exceptionHandler = () =>
        {
            var pageRequest = new PageRequest()
            {
                Page = -1,
                Size = 5,
            };
            var page = data.ToPagedList(pageRequest.Page, pageRequest.Size);
        };

        Assert.Throws<ArgumentException>(exceptionHandler);
    }
}
