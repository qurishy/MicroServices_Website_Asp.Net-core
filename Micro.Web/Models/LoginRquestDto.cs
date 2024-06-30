using System.ComponentModel.DataAnnotations;

namespace Micro.Web.Models
{
    public class LoginRquestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PassWord { get; set; }
    }
}
