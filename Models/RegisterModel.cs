using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Pole jest wymagane")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Hasło")]
        [Required(ErrorMessage ="Pole jest wymagane")]
        public string Password { get; set; }

        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasła muszą pasować")]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public string ConfirmPassword { get; set; }
    }
}
