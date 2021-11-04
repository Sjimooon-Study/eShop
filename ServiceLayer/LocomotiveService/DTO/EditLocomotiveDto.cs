using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static DataLayer.Models.Products.Locomotive;

namespace ServiceLayer.LocomotiveService
{
    public class EditLocomotiveDto : AddRollingStockDto, IHasId
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public EControl Control { get; set; }
        [Required]
        public ELocoType LocoType { get; set; }
        [Required]
        public bool AutoCoupling { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int NumOfDrivenAxels { get; set; }

        public int? DigitalDecoderId { get; set; }
    }
}
