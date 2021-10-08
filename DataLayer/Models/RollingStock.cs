using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Models.Products
{
    public abstract class RollingStock : ModelItem
    {
        public float Length { get; set; }
        public int NumOfAxels { get; set; }

        public int? RailwayCompanyId { get; set; }
        public RailwayCompany RailwayCompany { get; set; }
    }
}
