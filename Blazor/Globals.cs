using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor
{
    public static class Globals
    {
        // CORS
        public static readonly string LOCAL_API = "localapi";

        // Toast messages
        public static readonly string UNABLE_TO_FETCH_PRODUCTS = "Unable to fetch products: ";
        public static readonly string NOT_ENOUGH_STOCK = "Not enough stock.";

        // Toast headers
        public static readonly string SORRY = "SORRY";
        public static readonly string SYSTEM_ERROR = "SYSTEM ERROR";
    }
}
