using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceLayer.UserService.DTO
{
    public class SignInUserDto
    {
        [Display(Name = "E-mail/username")]
        [Required(ErrorMessage = "You must provide your e-mail or username.")]
        public string EmailUsername { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "You must provide your password.")]
        public string Password { get; set; }
    }
}
