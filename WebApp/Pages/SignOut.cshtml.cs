using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Helpers;

namespace WebApp.Pages
{
    public class SignOutModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            HttpContext.Session.Remove(Session.USER_ID);
            HttpContext.Session.Remove(Session.USERNAME);
            HttpContext.Session.Remove(Session.IS_ADMIN);

            return RedirectToPage();
        }
    }
}
