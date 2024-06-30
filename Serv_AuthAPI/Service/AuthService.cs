using Microsoft.AspNetCore.Identity;
using Serv_AuthAPI.DATABASE;
using Serv_AuthAPI.Models;
using Serv_AuthAPI.Models.DTO;
using Serv_AuthAPI.Service.IService;

namespace Serv_AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDBContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenreter _jwtTokenGenreter;
        public AuthService( ApplicationDBContext databa,UserManager< ApplicationUser >userm, RoleManager<IdentityRole> roles, IJwtTokenGenreter tok)
        {
            _db = databa;
            _userManager=userm;
            _jwtTokenGenreter=tok;
            _roleManager=roles;
        }
        //section for attending role to the users
        public async Task<bool> AssignRole(string email, string RoleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(RoleName).GetAwaiter().GetResult())
                {
                    //create role it it does not exist
                    _roleManager.CreateAsync(new IdentityRole(RoleName)).GetAwaiter().GetResult();

                }
                await _userManager.AddToRoleAsync(user,RoleName);
                return true;

            }
            return false;
        }





        //section for loging in and token creation
        public async Task<LoginResponsDto> Login(LoginRquestDto loginDto)
        {
           var user = _db.ApplicationUsers.FirstOrDefault(u=>u.UserName.ToLower() == loginDto.UserName.ToLower());

            bool isvalid = await _userManager.CheckPasswordAsync(user, loginDto.PassWord);

            if(user == null || isvalid== false)
            {
                
                return new LoginResponsDto()
                {
                    User = null,

                    Toekn=""
                };

            }

            //if user is found we have to create jwtToken
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenreter.GenerateToken(user, roles);

            UserDTO user1 = new()
            {
                Email=user.Email,
                Name = user.Name,
                ID = user.Id,
                PhoneNumber = user.PhoneNumber
                
                
            };




            LoginResponsDto reps = new LoginResponsDto()
            {
                User = user1,
                Toekn= token

            };

            return reps;

        }














        public async Task<string> Register(RegistrationRequistDto Registerdto)
        {
            ApplicationUser user = new()
            {
                UserName = Registerdto.Email,
                Email = Registerdto.Email,
                NormalizedEmail= Registerdto.Email.ToUpper(),
                Name = Registerdto.Name,
                PhoneNumber = Registerdto.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user,Registerdto.Password);


                if(result.Succeeded)
                {
                    var usertoreturn = _db.ApplicationUsers.First(u=>u.Email == Registerdto.Email);

                    return "";

                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }



            }catch(Exception ex) { 
            
            
            
            }
            return "error encounter";
        }
    }
}
