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
        private readonly DBContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AuthService(DBContext dbContext,
                          UserManager<IdentityUser> userManager,
                          SignInManager<IdentityUser> signInManager)

        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public Task Login(LoginModel model)
        {
            throw new NotImplementedException();
        }

        public Task Logout()
        {
            throw new NotImplementedException();
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
