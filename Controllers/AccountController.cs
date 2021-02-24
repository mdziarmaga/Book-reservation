using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService authService;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(IAuthService authService,
                                 SignInManager<IdentityUser> signInManager)
        {
            this.authService = authService;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                // var res = authService.Login(model);
                var res = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
              //  var id = authService.GetUserId();
              
                if(res.Succeeded)
                {
                 
                    return RedirectToAction("Index", "Home");
                }
                return View();
                
            }    
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel registerModel)
        {
            if(ModelState.IsValid)
            {
                authService.Register(registerModel);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Logout()
        {
            authService.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}
