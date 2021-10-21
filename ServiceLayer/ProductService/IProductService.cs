using ServiceLayer.ProductService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.ProductService
{
    public interface IProductService
    {
        public IQueryable<BasketProductDto> GetBasketProducts(SessionBasketDto basket);
    }
}
