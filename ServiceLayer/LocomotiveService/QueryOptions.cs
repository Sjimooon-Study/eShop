using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static ServiceLayer.Utilities.Pagination;

namespace ServiceLayer.LocomotiveService
{
    public enum EOrderByOptions
    {
        [Display(Name = "Name (Ascending)")]
        ByNameAsc = 0,
        [Display(Name = "Name (Descending)")]
        ByNameDesc,
        [Display(Name = "Price (Ascending)")]
        ByPriceAsc,
        [Display(Name = "Price (Descending)")]
        ByPriceDesc,
        [Display(Name = "Railway company (Ascending)")]
        ByRailwayCompanyAsc,
        [Display(Name = "Railway company (Descending)")]
        ByRailwayCompanyDesc
    }

    public class QueryOptions
    {
        public string SearchString { get; set; }
        public FilterOptions FilterOptions { get; set; }
        [Display(Name = "Order by")]
        public EOrderByOptions EOrderByOptions { get; set; }

        public ushort PageNumber { get; set; }
        [Display(Name = "Page size")]
        public EPageSize PageSize { get; set; } = EPageSize.PS10;
    }
}
