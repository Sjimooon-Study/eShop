using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class StockStatus
    {
        public int StockStatusId { get; set; }
        public uint Amount { get; set; }
        public DateTime? NextStock { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
