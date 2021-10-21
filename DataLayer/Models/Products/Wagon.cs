using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models.Products
{
    public class RailCar : RollingStock
    {
        public enum ERailCarType
        {
            PassengerCoach = 1,
            FreightWagon
        }
    }
}
