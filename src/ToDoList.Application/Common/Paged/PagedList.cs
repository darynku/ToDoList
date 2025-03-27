using Microsoft.EntityFrameworkCore;

namespace ToDoList.Application.Common.Paged;

public class PagedList<T>
{
    public IReadOnlyList<T> Items { get; init; } = [];

    public long TotalCount { get; init; }

    public int PageSize { get; init; }

    public int Page { get; init; }

    public bool HasNextPage => Page * PageSize < TotalCount;

    public bool HasPreviousPage => Page > 1;
    
}

public static class PagedListExtensions
{
    public static async Task<PagedList<T>> ToPagedList<T>(
        this IQueryable<T> source,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await source.CountAsync(cancellationToken);

        var items = await source
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedList<T>
        {
            Items = items, 
            PageSize = pageSize, 
            Page = page, 
            TotalCount = totalCount,
        };
    }

}

