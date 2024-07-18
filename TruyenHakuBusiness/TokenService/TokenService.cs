using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebCommon.Constants;
using WebModels.Entities;

namespace WebBusiness.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<UserAccount> _userManager;

        public TokenService(IConfiguration configuration, UserManager<UserAccount> userManager) 
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> GenerateToken(UserAccount user)
        {
            var newToken = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_configuration[Constants.AppSettingKeys.JWT_SECRET]);
     
            var roles =await _userManager.GetRolesAsync(user);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user?.UserName),
                    new Claim(ClaimTypes.Sid, user?.Id),
                    user?.Email != null ? new Claim(JwtRegisteredClaimNames.Email, user?.Email) : null,
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserName", user?.UserName),
                    new Claim("Id", user?.Id.ToString()),
                    //new Claim(ClaimTypes.Role,roles != null ?  string.Join(",",roles) : null)
                }),
                Expires = DateTime.Now.AddMinutes(double.Parse(_configuration[Constants.AppSettingKeys.JWT_EXPIREMINUTES])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            foreach (var userRole in roles) 
            {
                tokenDescription?.Subject?.AddClaim(new Claim(ClaimTypes.Role, userRole));
            }

            var token = newToken.CreateToken(tokenDescription);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
