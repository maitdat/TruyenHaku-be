using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebCommon.Constants;

namespace WebAPI.Middleware
{
    public class JwtMiddleware : IMiddleware
    {
        private readonly IConfiguration _configuration;
        public JwtMiddleware(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var test = context.Request.Cookies;
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                attachUserToContext(context,token);
            }
            await next(context);
        }

        private void attachUserToContext(HttpContext context,string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.UTF8.GetBytes(_configuration.GetSection(Constants.AppSettingKeys.JWT_SECRET).Value!);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                },
                out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken) validatedToken;
                var claims = jwtToken?.Claims.ToList();
                var user = new ClaimsIdentity(claims, ClaimTypes.Name);
                context.User = new ClaimsPrincipal(user);
            }
            catch
            {

            }
        }
    }
}
