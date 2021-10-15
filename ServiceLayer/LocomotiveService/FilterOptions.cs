using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceLayer.LocomotiveService
{
    public class FilterOptions
    {
        [Display(Name = "Tags")]
        public ICollection<string> Tags { get; set; }
    }
}
