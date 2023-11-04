using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Common.Response.PageListExtension
{
    public static class PagedListQueryableExtensions
    {
        public static async Task<DataList<TResult>> ToPagedListAsync<T,TResult>(
            this IQueryable<T> source,
            int page,
            int pageSize,
            IMapper _mapper,
            TResult result
            , CancellationToken token = default)
        {
            var count = await source.CountAsync(token);
            if (count == 0)
                return new DataList<TResult>(Enumerable.Empty<TResult>(), 0, 0, 0);

            var items = await source
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(token);
            var results = _mapper.Map(items, new List<TResult>());
            return new DataList<TResult>(results, count, page, pageSize);

        }
    }
}