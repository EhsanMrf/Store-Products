using Microsoft.EntityFrameworkCore;

namespace Common.Response.PageListExtension
{
    public static class PagedListQueryableExtensions
    {
        public static async Task<DataList<T>> ToPagedListAsync<T>(
            this IQueryable<T> source,
            int page,
            int pageSize,
            CancellationToken token = default)
        {
            var count = await source.CountAsync(token);
            if (count == 0)
                return new DataList<T>(Enumerable.Empty<T>(), 0, 0, 0);

            var items = await source
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(token);
            return new DataList<T>(items, count, page, pageSize);

        }
    }
}