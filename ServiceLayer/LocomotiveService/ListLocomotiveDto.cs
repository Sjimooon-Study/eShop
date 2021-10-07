using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.LocomotiveService
{
    public class ListLocomotiveDto
    {
        public string Name { get; set; }
        public string RailwayCompanyName { get; set; }
        public decimal Price { get; set; }
        public string Tag { get; set; }
        public StockStatusDto StockStatus { get; set; }
        public ICollection<ImageDto> Images { get; set; }
    }
}
