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
