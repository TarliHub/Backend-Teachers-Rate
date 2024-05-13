using AutoMapper;
using static System.Net.Mime.MediaTypeNames;

namespace TeacherRate.Api.Models.Paging;

public static class PageExtension
{
    public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = source.Skip(pageNumber * pageSize).Take(pageSize).ToList();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}
