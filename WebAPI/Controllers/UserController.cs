﻿using Microsoft.AspNetCore.Mvc;
using ServiceLayer.UserService;
using ServiceLayer.UserService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #region Post
        [HttpPost]
        [Route("user")]
        public IActionResult PostUserSignIn(SignInUserDto user)
        {
            if (_userService.SignIn(user, out SessionUserDto sessionUser))
            {
                return Ok(sessionUser);
            }
            
            return StatusCode(401);
        }
        #endregion
    }
}