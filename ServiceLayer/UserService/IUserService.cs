using ServiceLayer.UserService.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.UserService
{
    public interface IUserService
    {
        bool SignIn(SignInUserDto user, out SessionUserDto sessionUser);
        //int Add(AddUserDto user);
    }
}
