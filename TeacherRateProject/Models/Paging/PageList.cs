namespace TeacherRateProject.Models.Paging;

public class PageList<T>
{
    public List<T> Items { get; set; } = null!;
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int PagesCount { get; set; }
}
