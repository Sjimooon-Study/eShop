using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewComponents
{
    public class BasketButtonViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(HttpContext.GetBasket().Count);
        }
    }
}
