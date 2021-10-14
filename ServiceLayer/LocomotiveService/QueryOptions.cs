using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceLayer.LocomotiveService
{
    public enum OrderByOptions
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
        public OrderByOptions OrderByOptions { get; set; }
        public FilterOptions FilterOptions { get; set; }
    }
}
