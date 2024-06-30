using Serv_AuthAPI.Models.DTO;

namespace Serv_AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequistDto Registerdto);

        Task<LoginResponsDto> Login(LoginRquestDto loginDto);

        Task<bool>  AssignRole(string email, string RoleName);
    }
}
