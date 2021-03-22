using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class ResetPasswordModel
    {

        [Required(ErrorMessage ="Field is requires")]
        public string Password { get; set; }

        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Password not match")]
        [Required(ErrorMessage = "Field is requirede")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
        public string Email { get; set; }
    }
}
