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
        [BindProperty]
        public SignInUserDto SiteUser { get; set; } = new SignInUserDto();

        readonly IUserService _userService;

        public SignInModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (_userService.SignIn(SiteUser, out SessionUserDto sessionUser))
            {
                HttpContext.Session.SetInt32(Session.USER_ID, sessionUser.UserId);
                HttpContext.Session.SetString(Session.USERNAME, sessionUser.Username);
                HttpContext.Session.SetInt32(Session.IS_ADMIN, Convert.ToInt32(sessionUser.IsAdmin));
            }

            return RedirectToPage("/Index");
        }
    }
}
