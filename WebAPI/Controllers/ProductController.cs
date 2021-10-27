using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ProductService;
using ServiceLayer.ProductService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #region Get
        [HttpGet]
        [Route("basket")]
        public IEnumerable<BasketProductDto> GetBasketProducts()
        {
            //_productService.GetBasketProducts();

            throw new NotImplementedException();
        }

        /// <summary>
        /// Get current stock for specific product.
        /// </summary>
        /// <param name="id">Product ID.</param>
        /// <returns>Current stock for product.</returns>
        [HttpGet]
        [Route("stock/{id:int}")]
        public uint GetStock(int id)
        {
            return _productService.GetStock(id);
        }

        /*
        public async Task<IActionResult> Method(int productId, int imageNumber)
        // Get image DTO
        
        // Specify path
        
        var image = System.IO.File.OpenRead(path);
        return File(image, "image/jpeg")
        */
        #endregion
    }
}
