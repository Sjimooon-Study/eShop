using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Models
{
    public class Tag
    {
        [Key]
        public string TagId { get; set; }
    }
}
