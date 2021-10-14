using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer
{
    public class StockStatusDto
    {
        public uint Amount { get; set; }
        public DateTime? NextStock { get; set; }
        public bool InStock
        {
            get
            {
                return Amount > 0;
            }
        }
    }
}
