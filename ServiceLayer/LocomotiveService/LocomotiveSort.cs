using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.LocomotiveService
{
    public enum OrderByOptions
    {
        ByNameAsc = 0,
        ByNameDesc,
        ByPriceAsc,
        ByPriceDesc,
        ByRailwayCompanyAsc,
        ByRailwayCompanyDesc
    }

    public static class LocomotiveSort
    {
        /// <summary>
        /// Order list locomotives.
        /// </summary>
        /// <param name="locomotives">Locomotives to order.</param>
        /// <param name="orderByOptions">What property to order by.</param>
        /// <returns><see cref="IQueryable"/> of <see cref="ListLocomotiveDto"/>.</returns>
        public static IQueryable<ListLocomotiveDto> OrderLocomotivesBy(this IQueryable<ListLocomotiveDto> locomotives, OrderByOptions orderByOptions) => orderByOptions switch
        {
            OrderByOptions.ByNameAsc => locomotives.OrderBy(l => l.Name),
            OrderByOptions.ByNameDesc => locomotives.OrderByDescending(l => l.Name),
            OrderByOptions.ByPriceAsc => locomotives.OrderBy(l => l.Price),
            OrderByOptions.ByPriceDesc => locomotives.OrderByDescending(l => l.Price),
            OrderByOptions.ByRailwayCompanyAsc => locomotives.OrderBy(l => l.RailwayCompanyName),
            OrderByOptions.ByRailwayCompanyDesc => locomotives.OrderByDescending(l => l.RailwayCompanyName),
            _ => throw new ArgumentOutOfRangeException(nameof(orderByOptions), orderByOptions, null),
        };
    }
}
