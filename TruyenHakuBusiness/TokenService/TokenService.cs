using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TruyenHakuCommon.Constants;
using TruyenHakuModels.Entities.Account;

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

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        //public async Task<string> RefreshToken(string token)
        //{

        //}

        public List<Claim> CreateClaimIdentity(UserAccount user,IList<string> roles)
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
        #region Cookie
        public void SetTokenInsideCookie(string token, HttpContext httpContext)
        {
            httpContext.Response.Cookies.Append(Constants.Token.ACCESS_TOKEN, token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.Now.AddMinutes(20),
                SameSite = SameSiteMode.None,
                IsEssential = true,
            });

            httpContext.Response.Cookies.Append(Constants.Token.REFRESH_TOKEN, "abc", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.Now.AddDays(7),
                SameSite = SameSiteMode.None,
                IsEssential = true,
            });
        }
        #endregion


    }
}
