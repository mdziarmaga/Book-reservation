using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Field is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Hasło")]     
        [Required(ErrorMessage = "Field is required")]
        public string Password { get; set; }

        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Password not match")]
        [Required(ErrorMessage = "Field is requirede")]
        public string ConfirmPassword { get; set; }
    }
}
