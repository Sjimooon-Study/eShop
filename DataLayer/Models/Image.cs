using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public string Url { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
