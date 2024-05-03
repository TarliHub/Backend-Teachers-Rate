using TeacherRate.Api.Models.Requests;

namespace TeacherRate.Api.Models;

public class PageModel<T>
{
    public PageModel(int page, int size)
    {
        Page = page;
        Size = size;
    }

    public PageModel(PageRequest pageRequest)
    {
        Page = pageRequest.Page;
        Size = pageRequest.Size;
    }

    public IEnumerable<T> Items { get; set; } = null!;
    public int Page { get; set; }
    public int Size { get; set; }
}
