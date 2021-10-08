using DataLayer;
using DataLayer.Models;
using DataLayer.Models.Products;
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

        public IQueryable<ListLocomotiveDto> GetListLocomotives(QueryOptions queryOptions)
        {
            var locomotiveQuery = _context.Locomotives
                .AsNoTracking()
                .MapListLocomotiveToDto()
                .OrderLocomotivesBy(queryOptions.OrderByOptions);
            return locomotiveQuery;
        }

        public void Add(AddLocomotiveDto locomotive, bool saveChanges = true)
        {
            Locomotive locomotiveToAdd = new Locomotive()
                .MapProductProperties(locomotive)
                .MapModelItemProperties(locomotive)
                .MapRollingStockProperties(locomotive)
                .MapLocomotiveProperties(locomotive);

            _context.Locomotives.Add(locomotiveToAdd);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Add(List<AddLocomotiveDto> locomotives)
        {
            foreach (AddLocomotiveDto locomotive in locomotives)
            {
                Add(locomotive, false);
            }
            
            _context.SaveChanges();
        }
    }
}
