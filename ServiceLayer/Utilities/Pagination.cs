using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.Utilities
{
    public static class Pagination
    {
        public static IQueryable<T> Page<T>(this IQueryable<T> query, ref ushort pageNumber, ushort pageSize, out ushort numberOfPages)
        {
            if (pageSize == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size cannot be zero.");
            }

            numberOfPages = (ushort)Math.Ceiling((double)query.Count() / pageSize);
            pageNumber = Math.Min(Math.Max((ushort)1, pageNumber), numberOfPages);

            if (pageNumber > 1)
            {
                query = query.Skip((pageNumber - 1) * pageSize);
            }

            return query.Take(pageSize);
        }
    }
}
