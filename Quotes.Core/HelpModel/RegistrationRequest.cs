using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Core.HelpModel
{
    public class RegistrationRequest
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [StringLength(50, MinimumLength = 8, ErrorMessage = "Длина пароля должна быть от 8 до 50 символов")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        
        [Required(ErrorMessage = "ConfirmPassword is required")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
