using System;
using System.Collections.Generic;
using System.Text;
using static DataLayer.Models.Products.Locomotive;

namespace ServiceLayer.LocomotiveService
{
    public class AddLocomotiveDto : AddRollingStockDto
    {
        public EControl Control { get; set; }
        public ELocoType LocoType { get; set; }
        public bool AutoCoupling { get; set; }
        public int NumOfDrivenAxels { get; set; }

        public int? DigitalDecoderId { get; set; }
    }
}
