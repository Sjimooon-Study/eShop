using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ServiceLayer
{
    public abstract class AddProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<int> ReusedImages { get; set; } = new List<int>();
        public ICollection<AddImageDto> AddedImages { get; set; } = new List<AddImageDto>();
        public string Tag { get; set; }

        public AddStockStatusDto StockStatus { get; set; } = new AddStockStatusDto() { Amount = 0 };
    }
}
