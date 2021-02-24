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
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthService(UserManager<IdentityUser> userManager,
                          SignInManager<IdentityUser> signInManager,
                          IHttpContextAccessor httpContextAccessor)

        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        //public int GetUserId(string name)
        //{
        //    var u = userManager.FindByNameAsync(name);
        //    var userId = u.Id;
        //    //var userId = await userManager.GetUserIdAsync(HttpContext.User);
        //    return userId;

        //  //  var user = await GetUserId();
            
        //    //var s = signInManager.GetExternalLoginInfoAsync()
        //    //return user;
        //}

       // public  string GetUserId()
       //{
       // var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
       // var user = await userManager.FindByEmailAsync(User.Identity.Name);
       // var userId = await userManager.GetUserIdAsync(HttpContext.User);
       // return userId;
       //}

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
