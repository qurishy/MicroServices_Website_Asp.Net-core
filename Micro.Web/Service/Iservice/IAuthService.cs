using Micro.Web.Models;

namespace Micro.Web.Service.Iservice
{
    public interface IAuthService
    {
        Task<ResponsDTO> LoginAysnc(LoginRquestDto loginrequst);

        Task<ResponsDTO> RegisterAysnc(RegistrationRequistDto registerRequst);

        Task<ResponsDTO> AssignRoleAysnc(RegistrationRequistDto registration);
    }
}
