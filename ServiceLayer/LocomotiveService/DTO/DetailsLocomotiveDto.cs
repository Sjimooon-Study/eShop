using System;
using System.Collections.Generic;
using System.Text;
using static DataLayer.Models.ModelItem;
using static DataLayer.Models.Products.Locomotive;

namespace ServiceLayer.LocomotiveService
{
    public class DetailsLocomotiveDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public uint AmountInStock { get; set; }
        public ICollection<ImageDto> Images { get; set; }
        public string Tag { get; set; }

        public EScale Scale { get; set; }
        public EEpoch Epoch { get; set; }

        public float Length { get; set; }
        public int NumOfAxels { get; set; }
        public string RailwayCompanyName { get; set; }
        public string RailwatCompanyCountryName { get; set; }

        public EControl Control { get; set; }
        public ELocoType LocoType { get; set; }
        public bool AutoCoupling { get; set; }
        public int NumOfDrivenAxels { get; set; }
    }
}
