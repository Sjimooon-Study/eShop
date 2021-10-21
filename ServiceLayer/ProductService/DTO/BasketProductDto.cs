using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.ProductService.DTO
{
    public class BasketProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public ImageDto Image { get; set; }
        public int Count { get; set; }

        public decimal TotalPrice {
            get
            {
                return UnitPrice * Count;
            }
        }
    }
}
