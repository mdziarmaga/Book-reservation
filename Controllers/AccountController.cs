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
        private readonly UserManager<IdentityUser> userManager;

        public AccountController(IAuthService authService,
                                 SignInManager<IdentityUser> signInManager,
                                 UserManager<IdentityUser> userManager)
        {
            this.authService = authService;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            var reservationRequirement = TempData["ReservationRequirement"];
            if (reservationRequirement != null)
            {
                ViewData["ReservationRequirement"] = reservationRequirement;
            }

            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> Login(LoginModel model)
        {
         

            if (ModelState.IsValid)
            {
                // var res = authService.Login(model);
                
                var res = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                           
                if(res.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("Error", "Invalid login attepmt ");

                return View();
            }    
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
 
    [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if(ModelState.IsValid)
            {
                //authService.Register(registerModel);

                var userEmail = await userManager.FindByEmailAsync(registerModel.Email);

                if (userEmail != null)
                {
                    ModelState.AddModelError("Login", "Email exist.");
                }

                var identityUser = new IdentityUser
                {
                    UserName = registerModel.Email,
                    Email = registerModel.Email
                };

                var result = await userManager.CreateAsync(identityUser, registerModel.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.TryAddModelError("PasswordError", error.Description);
                    }
                    return View(registerModel);
                }
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
