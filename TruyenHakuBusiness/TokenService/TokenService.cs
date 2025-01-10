using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TruyenHakuCommon.Constants;
using TruyenHakuModels.Entities;

namespace TruyenHakuBusiness.TokenService
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

            var secretKeyBytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[Constants.AppSettingKeys.JWT_SECRET])) ;
     
            var roles =await _userManager.GetRolesAsync(user);

            var token = new JwtSecurityToken(
                issuer: _configuration[Constants.AppSettingKeys.JWT_VALIDISSUER],
                audience: _configuration[Constants.AppSettingKeys.JWT_VALIDAUDIENCE],
                expires: DateTime.Now.AddMinutes(20),
                claims: CreateClaimIdentity(user,roles),
                signingCredentials: new SigningCredentials(secretKeyBytes, SecurityAlgorithms.HmacSha512Signature)
            );

            //var tokenDescription = new SecurityTokenDescriptor
            //{
            //    Subject = CreateClaimIdentity(user,roles),
            //    Expires = DateTime.Now.AddMinutes(double.Parse(_configuration[Constants.AppSettingKeys.JWT_EXPIREMINUTES])),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            //};

     

            //var token = newToken.CreateToken(tokenDescription);

            return new JwtSecurityTokenHandler().WriteToken(token);


        }

        private List<Claim> CreateClaimIdentity(UserAccount user,IList<string> roles)
        {
            var claims = new List<Claim>();
            claims.AddRange([
                    new Claim(ClaimTypes.Name, user?.UserName),
                    new Claim(ClaimTypes.Sid, user?.Id),
                    user?.Email != null ? new Claim(JwtRegisteredClaimNames.Email, user?.Email) : null,
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserName", user?.UserName),
                    new Claim("Id", user?.Id.ToString())]
                    );
            foreach(var userRole in roles)
            {
                claims.Add(new Claim (ClaimTypes.Role, userRole));
            }
            
            return claims;
        }
        
        //public async Task<string> RefreshToken(string token)
        //{

        //}
    }
}
