using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer
{
    public abstract class AddRollingStockDto : AddModelItemDto
    {
        public float Length { get; set; }
        public int NumOfAxels { get; set; }

        public int? RailwayCompanyId { get; set; }
    }
}
