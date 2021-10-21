using DataLayer;
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
        public IQueryable<BasketProductDto> GetBasketProducts(SessionBasketDto basket) => 
            _context.Products
                .Where(p => basket.Products.Keys.Contains(p.ProductId))
                .Select(p => new BasketProductDto
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    UnitPrice = p.Price,
                    Image = p.Images.Select(i => new ImageDto { Path = i.Path }).FirstOrDefault(),
                    Count = basket.Products[p.ProductId]
                });
        #endregion
    }
}
