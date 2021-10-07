using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models.Products
{
    public class Locomotive : RollingStock
    {
        public enum EControl
        {
            AC = 1,
            ACC,
            DC,
            DCC
        }

        public enum ELocoType
        {
            SteamLocomotive = 1,
            DieselLocomotive,
            ElectricLocomotive,
            RailCar
        }

        public EControl Control { get; set; }
        public ELocoType LocoType { get; set; }
        public bool AutoCoupling { get; set; }
        public int NumOfDrivenAxels { get; set; }

        public int? DigitalDecoderId { get; set; }
        public DigitalDecoder DigitalDecoder { get; set; }
    }
}
