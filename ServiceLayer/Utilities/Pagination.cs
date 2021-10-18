using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ServiceLayer.Utilities
{
    public static class Pagination
    {
        public enum EPageSize : ushort
        {
            [Display(Name = "1")]
            PS1 = 1,
            [Display(Name = "5")]
            PS5 = 5,
            [Display(Name = "10")]
            PS10 = 10,
            [Display(Name = "20")]
            PS20 = 20,
            [Display(Name = "All")]
            PSALL = 0
        }

        public static IQueryable<T> Page<T>(this IQueryable<T> query, ref ushort pageNumber, ushort pageSize, out ushort numberOfPages)
        {
            if (pageSize == ((ushort)EPageSize.PSALL))
            {
                numberOfPages = 1;
                return query;
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
