using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models.Products
{
    public class DigitalDecoder : Product
    {
        public enum EDecoderInterface
        {
            PluX22
        }

        public EDecoderInterface Interface { get; set; }
        public bool Sound { get; set; }
    }
}
