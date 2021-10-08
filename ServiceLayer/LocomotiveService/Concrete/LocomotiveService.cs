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

        #region Add
        public int Add(AddLocomotiveDto locomotive, bool saveChanges = true)
        {
            Locomotive locomotiveToAdd = new Locomotive()
                .MapProductProperties(locomotive)
                .MapModelItemProperties(locomotive)
                .MapRollingStockProperties(locomotive)
                .MapLocomotiveProperties(locomotive);

            _context.Locomotives.Add(locomotiveToAdd);

            if (saveChanges)
            {
                return _context.SaveChanges();
            }

            return 0;
        }

        public int Add(List<AddLocomotiveDto> locomotives)
        {
            foreach (AddLocomotiveDto locomotive in locomotives)
            {
                Add(locomotive, false);
            }
            
            return _context.SaveChanges();
        }
        #endregion

        #region Get
        public DetailsLocomotiveDto GetDetailsLocomotive(int locomotiveId)
        {
            return _context.Locomotives.Find(locomotiveId)?
                .MapDetailsLocomotiveDto();
        }

        public IQueryable<ListLocomotiveDto> GetListLocomotives(QueryOptions queryOptions)
        {
            var locomotiveQuery = _context.Locomotives
                .AsNoTracking()
                .MapListLocomotiveToDto()
                .OrderLocomotivesBy(queryOptions.OrderByOptions);

            return locomotiveQuery;
        }
        #endregion

        #region Edit
        public int Edit(EditLocomotiveDto locomotive)
        {
            Locomotive locomotiveToUpdate = new Locomotive()
                .MapProductProperties(locomotive)
                .MapModelItemProperties(locomotive)
                .MapRollingStockProperties(locomotive)
                .MapLocomotiveProperties(locomotive);

            _context.Update(locomotiveToUpdate);

            return _context.SaveChanges();
        }
        #endregion

        #region Delete
        public int Delete(params int[] locomotiveIds)
        {
            _context.Locomotives.RemoveRange(_context.Locomotives.Where(l => locomotiveIds.Contains(l.ProductId)));

            return _context.SaveChanges();
        }
        #endregion
    }
}
