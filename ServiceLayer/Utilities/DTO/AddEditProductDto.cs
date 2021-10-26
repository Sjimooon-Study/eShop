using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ServiceLayer
{
    public abstract class AddEditProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public uint AmountInStock { get; set; }
        public ICollection<AddEditImageDto> Images { get; set; } = new List<AddEditImageDto>();
        public string Tag { get; set; }
    }
}
