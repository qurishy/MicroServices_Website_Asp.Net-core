using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serv_AuthAPI.Models;
using Serv_AuthAPI.Service.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace Serv_AuthAPI.Service
{
    public class JwtTokenGenreter : IJwtTokenGenreter
    {
        private readonly JwtOptions _jwtOptions;
        public JwtTokenGenreter(IOptions<JwtOptions> jwtOptions)
        {

            _jwtOptions=jwtOptions.Value;

        }


        public string GenerateToken(ApplicationUser aplicant, IEnumerable<string> roles)
        {
            var tokenhandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,aplicant.Email),
                 new Claim(JwtRegisteredClaimNames.Sub,aplicant.Id),
                  new Claim(JwtRegisteredClaimNames.Name,aplicant.UserName),
            };

            claims.AddRange(roles.Select(rol => new Claim(ClaimTypes.Role, rol)));

            var tokenDiscreptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenhandler.CreateToken(tokenDiscreptor);
            return tokenhandler.WriteToken(token);



        }
    }
}
