using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Models.Products
{
    public class Locomotive : RollingStock
    {
        public enum EControl
        {
            [Display(Name = "AC")]
            AC = 0,
            [Display(Name = "ACC")]
            ACC,
            [Display(Name = "DC")]
            DC,
            [Display(Name = "DCC")]
            DCC
        }

        public enum ELocoType
        {
            [Display(Name = "Steam")]
            SteamLocomotive = 0,
            [Display(Name = "Diesel")]
            DieselLocomotive,
            [Display(Name = "Electric")]
            ElectricLocomotive,
            [Display(Name = "Rail car")]
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
