using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AuthService(UserManager<IdentityUser> userManager,
                          SignInManager<IdentityUser> signInManager)

        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<SignInResult> Login(LoginModel model)
        {
            var res = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (res.Succeeded)
            {
                return SignInResult.Success;
            }
            return SignInResult.Failed;
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<AuthorizationResult> Register(RegisterModel model)
        {

            var userEmail = await userManager.FindByEmailAsync(model.Email);

            if (userEmail != null)
            {
                // ModelState.AddModelError("Login", "Email exist.");
                return AuthorizationResult.Failed();         
            }

            var identityUser = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(identityUser, model.Password);

            if (!result.Succeeded)
            {
                //foreach (var error in result.Errors)
                //{
                //    ModelState.TryAddModelError("PasswordError", error.Description);
                //}

                return AuthorizationResult.Failed();      
            }
            return AuthorizationResult.Success();
        }


    }
}
