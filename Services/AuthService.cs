using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task Login(LoginModel model)
        {
            var user = userManager.FindByEmailAsync(model.Email);

            if(user != null)
            {
               var res= await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
             
            }
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        public async Task Register(RegisterModel model)
        {
            //var userEmail = await userManager.FindByEmailAsync(model.Email);


            var identityUser = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
              
            };

            var result = await userManager.CreateAsync(identityUser, model.Password);

        }
    }
}
