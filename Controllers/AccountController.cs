using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService authService;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly IEmailService emailAuth;

        public AccountController(IAuthService authService,
                                 SignInManager<IdentityUser> signInManager,
                                 UserManager<IdentityUser> userManager,
                                 IConfiguration configuration,
                                 IEmailService emailAuth)

        {
            this.authService = authService;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
            this.emailAuth = emailAuth;
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
                var res = authService.Login(model);

                if(res.Result.Succeeded)
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

                var addUserToRole = await userManager.AddToRoleAsync(identityUser, "User");

                if(!addUserToRole.Succeeded)
                {
                    foreach (var item in addUserToRole.Errors)
                    {
                        ModelState.TryAddModelError("RoleError", item.Description);
                    }
                    return View(registerModel);
                }

                var token = await userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                // var confirmationLink = Url.Action("ConfirmEmail", "Account", new { token, email = identityUser.Email }, Request.Scheme);

                var encodedtoken = Encoding.UTF8.GetBytes(token);
                var validtoken = WebEncoders.Base64UrlEncode(encodedtoken);
               
                string confirmationLink = $"{configuration["AppUrl"]}/Account/ConfirmEmail?email={identityUser.Email}&token={validtoken}";
                await emailAuth.SendEmailAsync(identityUser.Email, "Potwierdź email", $"<h1>Witam, proszę potwierdzić email </h1> <a href='{confirmationLink}'> kliknij tutaj </a>");

                return RedirectToAction("Index", "Home");

                //var res = authService.Register(registerModel);

                //if (res.Result.Succeeded)
                //{
                //    return RedirectToAction("Index", "Home");
                //}
                //return View();
            }
            return View();
        }

        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var decodedToken = WebEncoders.Base64UrlDecode(token);
                string normalToken = Encoding.UTF8.GetString(decodedToken);
                var resullt = await userManager.ConfirmEmailAsync(user, normalToken);
            }
            return View();
        }

        [HttpGet]
        public IActionResult ForgetPassword( )
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            if (!ModelState.IsValid)
                return View();

            var res = authService.ForgetPassword(forgetPasswordModel);
            if(await res)
                return RedirectToAction("ForgetPasswordConfirmation");
            return View();
        }

        [HttpGet]
        public IActionResult ForgetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email) //ResetPasswordModel resetModel
        {
            var model = new ResetPasswordModel { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword( ResetPasswordModel resetModel)
        {
            if (!ModelState.IsValid)
                return View();

            var result = authService.ResetPassword(resetModel);

            if(await result)
                return RedirectToAction("ResetPasswordConfirmation");
            return View();
        }

        
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        public IActionResult Logout()
        {
            authService.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}
