using DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.LocomotiveService.Concrete
{
    public class LocomotiveService
    {
        private readonly EShopContext _context;
        public LocomotiveService(EShopContext context)
        {
            _context = context;
        }

        public IQueryable<ListLocomotiveDto> GetListLocomotives()
        {
            var locomotiveQuery = _context.Locomotives
                .AsNoTracking()
                .MapListLocomotiveToDto();
            return locomotiveQuery;
        }
    }
}
