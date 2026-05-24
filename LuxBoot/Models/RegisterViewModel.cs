using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LuxBoot.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(16)]
        [MinLength(5)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(16)]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [MaxLength(16)]
        [MinLength(8)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
