using Microsoft.AspNetCore.Identity;

namespace Serv_AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
