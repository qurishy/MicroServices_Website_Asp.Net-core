using Micro.Web.Models;
using Micro.Web.Service.Iservice;
using Micro.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Micro.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        public AuthController(IAuthService Auth, ITokenProvider tokenProvider)
        {
            _authService = Auth;

            _tokenProvider=tokenProvider;

        }



        [HttpGet]
        public IActionResult Login() 
        {
            LoginRquestDto loginRequist = new();

            return View(loginRequist);

        
        
        }


        [HttpPost]
        public async Task< IActionResult> Login(LoginRquestDto obj)
        {


            ResponsDTO respon = await _authService.LoginAysnc(obj);

            

            if (respon.result != null && respon.Issuccess)
            {
              LoginResponsDto reslt = 
                    JsonConvert.DeserializeObject<LoginResponsDto>(Convert.ToString(respon.result));

                await SignInUser(reslt);
                _tokenProvider.SetToken(reslt.Toekn);
                
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Customerror", respon.Message);
                TempData["error"] = "Fail Attempt";
                return View(obj);

            }


        }




        [HttpGet]
        public IActionResult Register()
        {
            var rolelist = new List<SelectListItem>()
            {
                new SelectListItem( SD.RoleAdmin, SD.RoleAdmin),
                new SelectListItem( SD.RoleCutomer, SD.RoleCutomer)


            };
            ViewBag.rolelist = rolelist;

            return View();


        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequistDto obj)
        {
            ResponsDTO respon = await _authService.RegisterAysnc(obj);

            ResponsDTO rolegive;

            if (respon != null && respon.Issuccess== true)
            {
                if(string.IsNullOrEmpty(obj.Role))
                {
                    obj.Role=SD.RoleCutomer;
                }
                rolegive = await _authService.AssignRoleAysnc(obj);
                if(rolegive != null && rolegive.Issuccess)
                {
                    TempData["success"]="Registration Successsful";
                    return RedirectToAction(nameof(Login));
                }

            }
            else
            {
                TempData["error"]= respon.Message;
            }

            var rolelist = new List<SelectListItem>()
            {
                new SelectListItem( SD.RoleAdmin, SD.RoleAdmin),
                new SelectListItem( SD.RoleCutomer, SD.RoleCutomer)


            };
            ViewBag.rolelist = rolelist;

            return View(obj);


        }






        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearedToken();

            return RedirectToAction("Index", "Home");


        }



        private async Task SignInUser(LoginResponsDto obj)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(obj.Toekn);

            var identiry = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identiry.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(u => u.Type==JwtRegisteredClaimNames.Email).Value));

            identiry.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type==JwtRegisteredClaimNames.Sub).Value));

            identiry.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type==JwtRegisteredClaimNames.Name).Value));


            identiry.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type==JwtRegisteredClaimNames.Email).Value));

            identiry.AddClaim(new Claim(ClaimTypes.Role,
                jwt.Claims.FirstOrDefault(u => u.Type== "role").Value));


            var principal = new ClaimsPrincipal(identiry);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

    }
}
