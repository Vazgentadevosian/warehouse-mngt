using WRMNGT.Infrastructure.Models.Response;

namespace WRMNGT.Infrastructure.Helpers
{
    public static class PagingHelper
    {
        /// <param name="page">Counting starts from 1. Returns </param>
        /// <returns></returns>
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int? page, int pageSize)
        {
            if (!page.HasValue)
                return query;

            page--;
            return query.Skip(page.Value * pageSize).Take(pageSize);
        }

        public static PaginatedResult<T> Pagination<T>(this IEnumerable<T> data, int page, int pageSize, int count)
            => new PaginatedResult<T>
            {
                Data = data.ToList(),
                Page = page,
                PageSize = pageSize,
                Count = count,
            };
    }
}
