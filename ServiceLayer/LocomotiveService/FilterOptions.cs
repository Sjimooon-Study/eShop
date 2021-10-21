using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static DataLayer.Models.ModelItem;
using static DataLayer.Models.Products.Locomotive;

namespace ServiceLayer.LocomotiveService
{
    public class FilterOptions
    {
        [Display(Name = "Tag")]
        public ICollection<string> Tags { get; set; }
        [Display(Name = "Scale")]
        public ICollection<EScale> Scales { get; set; }
        [Display(Name = "Epoch")]
        public ICollection<EEpoch> Epochs { get; set; }
        [Display(Name = "Control")]
        public ICollection<EControl> Controls { get; set; }
        [Display(Name = "Locomotive type")]
        public ICollection<ELocoType> LocoTypes { get; set; }
    }
}
