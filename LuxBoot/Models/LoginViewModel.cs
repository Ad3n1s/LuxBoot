using System.ComponentModel.DataAnnotations;

namespace LuxBoot.Models
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(16)]
        [MinLength(5)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(16)]
        [MinLength(5)]
        public string Password { get; set; }
    }
}
