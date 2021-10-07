using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class RailwayCompany
    {
        public int RailwayCompanyId { get; set; }
        public string Name { get; set; }
        
        public int? CountryId { get; set; }
        public Country Country { get; set; }
    }
}
