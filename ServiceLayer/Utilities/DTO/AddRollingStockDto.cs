using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceLayer
{
    public abstract class AddRollingStockDto : AddEditModelItemDto
    {
        [Required, Range(0, float.MaxValue)]
        public float Length { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int NumOfAxels { get; set; }

        public int? RailwayCompanyId { get; set; }
    }
}
