using Serv_AuthAPI.Models;

namespace Serv_AuthAPI.Service.IService
{
    public interface IJwtTokenGenreter
    {
        string GenerateToken(ApplicationUser aplicant, IEnumerable<string> roles);
    }
}
