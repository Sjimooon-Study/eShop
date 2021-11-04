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

        /// <summary>
        /// Get all product tags.
        /// </summary>
        /// <returns>List of product tags.</returns>
        [HttpGet]
        [Route("tags")]
        public List<string> GetTags()
        {
            return _productService.GetTags().ToList();
        }

        /*
        public async Task<IActionResult> Method(int productId, int imageNumber)
        // Get image DTO
        
        // Specify path
        
        var image = System.IO.File.OpenRead(path);
        return File(image, "image/jpeg")
        */
        #endregion

        #region Put
        /// <summary>
        /// Get list of <see cref="BasketProductDto"/> from <see cref="SessionBasketDto"/>.
        /// </summary>
        /// <param name="basket"><see cref="BasketProductDto"/> with products.</param>
        /// <returns>List of <see cref="BasketProductDto"/>.</returns>
        [HttpPut]
        [Route("basket")]
        public List<BasketProductDto> GetBasketProducts(SessionBasketDto basket) =>
            _productService.GetBasketProducts(basket).ToList();
        #endregion
    }
}
