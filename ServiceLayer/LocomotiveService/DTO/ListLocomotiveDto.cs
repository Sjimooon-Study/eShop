using System;
using System.Collections.Generic;
using System.Text;
using static DataLayer.Models.ModelItem;
using static DataLayer.Models.Products.Locomotive;

namespace ServiceLayer.LocomotiveService
{
    public class ListLocomotiveDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<ImageDto> Images { get; set; }
        public string Tag { get; set; }
        public StockStatusDto StockStatus { get; set; }

        public EScale Scale { get; set; }
        public EEpoch Epoch { get; set; }
        
        public string RailwayCompanyName { get; set; }

        public EControl Control { get; set; }
        public ELocoType LocoType { get; set; }

    }
}
