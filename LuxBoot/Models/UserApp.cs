using Microsoft.AspNetCore.Identity;


namespace LuxBoot.Models
{
    public class UserApp : IdentityUser
    {
        public AccountInfoModel AccountInfo { get; set; }
    }
}
