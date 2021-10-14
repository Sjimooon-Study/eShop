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
    public class LocomotiveService : ILocomotiveService
    {
        private readonly EShopContext _context;
        public LocomotiveService(EShopContext context)
        {
            _context = context;
        }

        #region Add
        /// <summary>
        /// Add single <see cref="Locomotive"/> to underlying database.
        /// </summary>
        /// <param name="locomotive">Locomotive to add.</param>
        /// <param name="saveChanges">Whether to save changes or not.</param>
        /// <returns>Number of affected entries.</returns>
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

        /// <summary>
        /// Add multiple <see cref="Locomotive"/>s to underlying database.
        /// </summary>
        /// <param name="locomotives">Locomotives to add.</param>
        /// <returns>Number of affected entries.</returns>
        public int Add(ICollection<AddLocomotiveDto> locomotives)
        {
            foreach (AddLocomotiveDto locomotive in locomotives)
            {
                Add(locomotive, false);
            }
            
            return _context.SaveChanges();
        }
        #endregion

        #region Get
        /// <summary>
        /// Get all locomotive details from underlying databse.
        /// </summary>
        /// <param name="locomotiveId"><see cref="Locomotive"/> ID</param>
        /// <returns>Locomotive details of <see cref="Locomotive"/> with <paramref name="locomotiveId"/>; otherwise null.</returns>
        public DetailsLocomotiveDto GetDetailsLocomotive(int locomotiveId)
        {
            return _context.Locomotives
                .Find(locomotiveId)?
                .MapDetailsLocomotiveDto();
        }

        /// <summary>
        /// Get queryable to list locomotives on a product page.
        /// </summary>
        /// <param name="queryOptions">Options including ordering, filters, and paging.</param>
        /// <returns><see cref="IQueryable"/> of <see cref="ListLocomotiveDto"/>.</returns>
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
        /// <summary>
        /// Edit <see cref="Locomotive"/> in underlying database.
        /// </summary>
        /// <param name="locomotive">Locomotive to edit.</param>
        /// <returns>Number of affected entries.</returns>
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
        /// <summary>
        /// Delete one or more <see cref="Locomotive"/> in underlying database.
        /// </summary>
        /// <param name="locomotiveIds"><see cref="Locomotive"/> ID(s)</param>
        /// <returns>Number of affected entries.</returns>
        public int Delete(params int[] locomotiveIds)
        {
            _context.Locomotives.RemoveRange(_context.Locomotives.Where(l => locomotiveIds.Contains(l.ProductId)));

            return _context.SaveChanges();
        }
        #endregion
    }
}
