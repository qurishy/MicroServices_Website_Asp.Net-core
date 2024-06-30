using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serv_AuthAPI.Models.DTO;
using Serv_AuthAPI.Service.IService;

namespace Serv_AuthAPI.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ResponsDTO _respon;

        public AuthApiController( IAuthService ser)
        {
            _authService = ser;
            _respon = new();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequistDto model)
        {
            var errormessage = await _authService.Register(model);

            if (!string.IsNullOrEmpty(errormessage))
            {
                _respon.Issuccess=false;
                _respon.Message=errormessage;
                return BadRequest(_respon);

            }

            return Ok(_respon);
                
        }



        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRquestDto model)
        {
            var loger = await _authService.Login(model);
            
            if(loger.User == null)
            {
                _respon.Issuccess = false;
                _respon.Message="Username or password is InCorrect";
                return BadRequest(_respon);
            }
            _respon.result = loger;


            return Ok(_respon) ;

        }






        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody]RegistrationRequistDto model)
        {
            var assignroler = await _authService.AssignRole(model.Email, model.Role.ToUpper());

            if (!assignroler)
            {
                _respon.Issuccess = false;
                _respon.Message="error encounter";
                return BadRequest(_respon);
            }
            _respon.result = assignroler;


            return Ok(_respon);

        }
    }
}
