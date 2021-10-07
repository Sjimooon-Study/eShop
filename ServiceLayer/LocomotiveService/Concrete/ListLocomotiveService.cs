using DataLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.LocomotiveService.Concrete
{
    public class ListLocomotiveService
    {
        private readonly EShopContext _context;
        public ListLocomotiveService(EShopContext context)
        {
            _context = context;
        }


    }
}
