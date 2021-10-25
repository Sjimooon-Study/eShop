using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.UserService;
using ServiceLayer.UserService.DTO;
using WebApp.Helpers;

namespace WebApp.Pages
{
    public class SignInModel : PageModel
    {
        private readonly string _WRONG_CREDENTIALS_MSG = "Wrong e-mail/username or password.";

        [BindProperty]
        public SignInUserDto SiteUser { get; set; } = new SignInUserDto();
        
        public string ValidationMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost([FromServices] IUserService userService) // Method injection - alternative to dependency injection.
        {
            ValidationMessage = "";

            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            if (userService.SignIn(SiteUser, out SessionUserDto sessionUser))
            {
                HttpContext.Session.SetInt32(Session.USER_ID, sessionUser.UserId);
                HttpContext.Session.SetString(Session.USERNAME, sessionUser.Username);
                HttpContext.Session.SetInt32(Session.IS_ADMIN, Convert.ToInt32(sessionUser.IsAdmin));
                
                return RedirectToPage("/Index");
            }
            else
            {
                ValidationMessage = _WRONG_CREDENTIALS_MSG;

                return Page();
            }
        }
    }
}
