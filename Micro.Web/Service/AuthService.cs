using Micro.Web.Models;
using Micro.Web.Service.Iservice;
using Micro.Web.Utility;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Win32;

namespace Micro.Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService servicebase)
        {
            _baseService = servicebase;
            
        }

        public async Task<ResponsDTO> AssignRoleAysnc(RegistrationRequistDto registration)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                apiType= Utility.SD.ApiType.POST,
                Data=  registration,
                Url= SD.AuthBaseApi+"/api/auth/AssignRole"

            });
        }

        public async Task<ResponsDTO> LoginAysnc(LoginRquestDto loginrequst)
        {

            return await _baseService.SendAsync(new RequestDto
            {
                apiType= Utility.SD.ApiType.POST,
                Data=  loginrequst,
                Url= SD.AuthBaseApi+"/api/auth/Login"

            }, withBearer: false);
        }

        public async Task<ResponsDTO> RegisterAysnc(RegistrationRequistDto registerRequst)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                apiType= Utility.SD.ApiType.POST,
                Data=  registerRequst,
                Url= SD.AuthBaseApi+"/api/auth/Register"

            },withBearer:false);
        }
    }
}
