using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.ProductService;
using ServiceLayer.ProductService.DTO;
using WebApp.Helpers;

namespace WebApp.Pages
{
    public class BasketModel : PageModel
    {
        public SessionBasketDto Basket { get; set; }
        public decimal TotalBasketPrice { get; set; }
        [BindProperty]
        public List<BasketProductDto> BasketProducts { get; set; }

        readonly IProductService _productService;

        public BasketModel(IProductService productService)
        {
            _productService = productService;
        }

        public void OnGet()
        {
            Basket = HttpContext.GetBasket();

            BasketProducts = _productService.GetBasketProducts(Basket).ToList();

            TotalBasketPrice = 0;
            foreach (BasketProductDto basketProduct in BasketProducts)
            {
                TotalBasketPrice += basketProduct.TotalPrice;
            }
        }

        public IActionResult OnGetRemove(int id)
        {
            Basket = HttpContext.GetBasket();

            Basket.Products.Remove(id);

            HttpContext.SetBasket(Basket);

            return RedirectToPage();
        }

        public IActionResult OnPostUpdate(int id, int count)
        {
            Basket = HttpContext.GetBasket();

            if (Basket.Products.ContainsKey(id))
            {
                Basket.Products[id] = count;

                HttpContext.SetBasket(Basket);
            }

            return RedirectToPage();
        }
    }
}
