using System;
using System.Collections.Generic;
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

    public class QueryOptions
    {
        public OrderByOptions OrderByOptions { get; set; }
    }
}
