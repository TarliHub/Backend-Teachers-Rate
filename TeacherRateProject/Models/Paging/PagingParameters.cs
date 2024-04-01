namespace TeacherRateProject.Models.Paging;

public class PagingParameters
{
    const int maxPageCount = 50;
    public int PageIndex { get; set; } = 1;
    private int _pageSize;
    public int PageSize {
        get => _pageSize;
        set => _pageSize = Math.Min(value, maxPageCount);
    }
}
