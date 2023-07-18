using System.Collections.Generic;

namespace WRMNGT.Infrastructure.Models.Response
{
    public class PaginatedResult<T>
    {
        public int PageSize { get; set; }

        public int Page { get; set; }

        public int Count { get; set; }

        public List<T> Data { get; set; }
    }
}
