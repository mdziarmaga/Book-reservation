using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IAuthService
    {
        Task<SignInResult> Login(LoginModel model);
        //Task Register(RegisterModel model);
        Task Logout();
        Task<AuthorizationResult> Register(RegisterModel model);
        // int GetUserId(string name);

        //Task ResetPassword(string email,ResetPasswordModel model);
    }
}
