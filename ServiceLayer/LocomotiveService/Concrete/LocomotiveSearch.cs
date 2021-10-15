using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.LocomotiveService
{
    public static class LocomotiveSearch
    {
        /// <summary>
        /// Order locomotives.
        /// </summary>
        /// <param name="locomotives">Locomotives to order.</param>
        /// <param name="orderByOptions">What property to order by.</param>
        /// <returns><see cref="IQueryable"/> of <see cref="ListLocomotiveDto"/>.</returns>
        public static IQueryable<ListLocomotiveDto> OrderLocomotivesBy(this IQueryable<ListLocomotiveDto> locomotives, EOrderByOptions orderByOptions) => orderByOptions switch
        {
            EOrderByOptions.Default => locomotives,
            EOrderByOptions.ByNameAsc => locomotives.OrderBy(l => l.Name),
            EOrderByOptions.ByNameDesc => locomotives.OrderByDescending(l => l.Name),
            EOrderByOptions.ByPriceAsc => locomotives.OrderBy(l => l.Price),
            EOrderByOptions.ByPriceDesc => locomotives.OrderByDescending(l => l.Price),
            EOrderByOptions.ByRailwayCompanyAsc => locomotives.OrderBy(l => l.RailwayCompanyName),
            EOrderByOptions.ByRailwayCompanyDesc => locomotives.OrderByDescending(l => l.RailwayCompanyName),
            _ => throw new ArgumentOutOfRangeException(nameof(orderByOptions), orderByOptions, null),
        };

        /// <summary>
        /// Search for locomotives using a search string. Effectively filters out non-matching objects.
        /// Whitespaces in <paramref name="searchString"/> is used to separate multiple search parameters, ie. "BR 218 DB" will search for both "BR", "218", and "DB".
        /// </summary>
        /// <param name="locomotives">Locomotives to search in.</param>
        /// <param name="searchString">Search string.</param>
        /// <returns><see cref="IQueryable"/> of <see cref="ListLocomotiveDto"/>.</returns>
        public static IQueryable<ListLocomotiveDto> SearchFor(this IQueryable<ListLocomotiveDto> locomotives, string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return locomotives;
            }
            
            string[] searchParams = searchString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            foreach (string searchParam in searchParams)
            {
                locomotives = locomotives
                    .Where(l => l.Name.Contains(searchParam)
                    || l.RailwayCompanyName.Contains(searchParam)
                    );
            }

            return locomotives;
        }

        /// <summary>
        /// Filter locomotives.
        /// </summary>
        /// <param name="locomotives">Locomotives to filter.</param>
        /// <param name="filterOptions">What filters to filter by.</param>
        /// <returns><see cref="IQueryable"/> of <see cref="ListLocomotiveDto"/>.</returns>
        public static IQueryable<ListLocomotiveDto> Filter(this IQueryable<ListLocomotiveDto> locomotives, FilterOptions filterOptions)
        {
            if (filterOptions == null)
            {
                return locomotives;
            }

            return locomotives
                .Where(l => (!filterOptions.Tags.Any() || filterOptions.Tags.Contains(l.Tag)));
        }
    }
}
