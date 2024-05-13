using AutoMapper;

namespace TeacherRate.Api.Models.Paging;

public class PagedList<T>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public List<T> Items { get; set; }

    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        Items = new List<T>(items);
    }

    public PagedList<TDest> Map<TDest>(IMapper mapper)
    {
        var items = mapper.Map<List<TDest>>(this.Items);
        return new PagedList<TDest>(items, this.TotalCount, this.CurrentPage, this.PageSize);
    }
}