using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewComponents
{
    public class SignInButtonViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            int? userId = HttpContext.Session.GetInt32(Session.USER_ID);
            string username = HttpContext.Session.GetString(Session.USERNAME);

            bool isLoggedIn;

            if (userId != null && userId > 0)
            {
                isLoggedIn = true;
            }
            else
            {
                isLoggedIn = false;
            }

            return View(new ValueTuple<bool, string>(isLoggedIn, username));
        }
    }
}
