using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.ProductService.DTO
{
    public class SessionBasketDto
    {
        public Dictionary<int, int> Products { get; set; } = new Dictionary<int, int>();
        public int Count
        {
            get
            {
                int count = 0;
                foreach (var key in Products.Keys)
                {
                    count += Products[key];
                }

                return count;
            }
        }
    }
}
