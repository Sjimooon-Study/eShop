using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Models
{
    public abstract class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(8, 2)")] // precision of 8 and scale of 2: 123456.78
        public decimal Price { get; set; }
        public uint AmountInStock { get; set; }

        public ICollection<Image> Images { get; set; }

        public string? TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
