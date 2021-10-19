using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceLayer.UserService.DTO
{
    public class SignInUserDto
    {
        [Display(Name = "Email/username")]
        public string EmailUsername { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
