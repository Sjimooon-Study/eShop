using DataLayer;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ProductService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.ProductService.Concrete
{
    public class ProductService : IProductService
    {
        private readonly EShopContext _context;
        public ProductService(EShopContext context)
        {
            _context = context;
        }

        #region Get
        /// <summary>
        /// Get queryable of basket products (details, price, and quantity).
        /// </summary>
        /// <param name="basket"><see cref="SessionBasketDto"/> to read IDs and quantity from.</param>
        /// <returns>Queryable of <see cref="BasketProductDto"/> from products in <paramref name="basket"/>.</returns>
        public IQueryable<BasketProductDto> GetBasketProducts(SessionBasketDto basket) => _context.Products
            .AsNoTracking()
            .Where(p => basket.Products.Keys.Contains(p.ProductId))
            .Select(p => new BasketProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                UnitPrice = p.Price,
                Image = p.Images.Select(i => new ImageDto { Path = i.Path }).FirstOrDefault(),
                Count = basket.Products[p.ProductId]
            });

        /// <summary>
        /// Get current stock of a product.
        /// </summary>
        /// <param name="productId">ID of product.</param>
        /// <returns>Amount in stock.</returns>
        public uint GetStock(int productId) => _context.Products
            .AsNoTracking()
            .Where(p => p.ProductId == productId)
            .Select(p => p.AmountInStock)
            .FirstOrDefault();
            
        #endregion
    }
}
