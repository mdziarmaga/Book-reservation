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
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace Library.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly IEmailService emailAuth;

        public AuthService(UserManager<IdentityUser> userManager,
                          SignInManager<IdentityUser> signInManager,
                          IConfiguration configuration,
                          IEmailService emailAuth)

        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.emailAuth = emailAuth;
        }

        public async Task<bool> ForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            var user = await userManager.FindByEmailAsync(forgetPasswordModel.Email);
           
            if (user != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                var encodedtoken = Encoding.UTF8.GetBytes(token);
                var validtoken = WebEncoders.Base64UrlEncode(encodedtoken);

                //  string confirmationLink = $"{configuration["AppUrl"]}/Account/ConfirmEmail?email={identityUser.Email}&token={validtoken}";
                var forgetPasswordLink = $"{configuration["AppUrl"]}/Account/ResetPassword?email={user.Email}&token={validtoken}";

                //var forgetPasswordLink = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);
                await emailAuth.SendEmailAsync(user.Email, "Password reset", $"<h1>Aby zresetować hasło  </h1> <a href='{forgetPasswordLink}'> kliknij tutaj </a>");
                return true;
            }
            return false;
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
            string url = $"{configuration["AppUrl"]}/ConfirmEmail?email={identityUser.Email}";

            await emailAuth.SendEmailAsync(identityUser.Email, "Potwierdź email", $"<h1>Witam, proszę potwierdzić email </h1> <a href='{url}'> kliknij tutaj </a>");

            return AuthorizationResult.Success();
        }

        public async Task<bool> ResetPassword(ResetPasswordModel resetModel)
        {
            var user = await userManager.FindByEmailAsync(resetModel.Email);

            if (user != null)
            {
                var decodedToken = WebEncoders.Base64UrlDecode(resetModel.Token);
                string normalToken = Encoding.UTF8.GetString(decodedToken);

                var resetPassword = await userManager.ResetPasswordAsync(user, normalToken, resetModel.Password);
                if (!resetPassword.Succeeded)
                {
                    //foreach (var error in resetPassword.Errors)
                    //{
                    //    ModelState.TryAddModelError(error.Code, error.Description);
                    //}
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
