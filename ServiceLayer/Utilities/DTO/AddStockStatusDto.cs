using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer
{
    public class AddStockStatusDto
    {
        public AddStockStatusDto()
        {
            Amount = 0;
        }

        public uint Amount { get; set; }
        public DateTime? NextStock { get; set; }
    }
}
