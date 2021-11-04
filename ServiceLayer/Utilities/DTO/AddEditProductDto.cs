using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceLayer
{
    public abstract class AddEditProductDto
    {
        [Required, MaxLength(256)]
        public string Name { get; set; }
        [Required, MaxLength(1024)]
        public string Description { get; set; }
        [Required, Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Required, Range(0, uint.MaxValue)]
        public uint AmountInStock { get; set; }
        public ICollection<AddEditImageDto> Images { get; set; } = new List<AddEditImageDto>();
        public string Tag { get; set; }
    }
}
