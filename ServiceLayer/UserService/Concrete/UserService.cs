using DataLayer;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.UserService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.UserService.Concrete
{
    public class UserService : IUserService
    {
        private readonly EShopContext _context;
        public UserService(EShopContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Log in a user by checking underlying database for email/username and password match.
        /// </summary>
        /// <param name="sessionUser">User to be logged in.</param>
        /// <returns>Whether user can log in or not.</returns>
        public bool SignIn(SignInUserDto user, out SessionUserDto sessionUser)
        {
            try
            {
                sessionUser = _context.Users
                    .AsNoTracking()
                    .Where(u =>
                    (u.UserName == user.EmailUsername || u.Email == user.EmailUsername) && u.Password == user.Password)
                    .Select(u => 
                    new SessionUserDto {
                        UserId = u.UserId,
                        Username = u.UserName,
                        IsAdmin = u.IsAdmin
                    })
                    .Single();
            }
            catch (Exception e)
            {
                sessionUser = new SessionUserDto
                {
                    UserId = 0,
                    Username = "Unknown",
                    IsAdmin = false
                };

                // TODO: Log exception

                return false;
            }

            return true;
        }
    }
}
