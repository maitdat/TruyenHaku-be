using WebBusiness.AuthService;
using WebBusiness.RoleService;
using WebBusiness.TokenService;
using WebBusiness.UserService;

namespace WebAPI.Extensions
{
    public static class ServicesRegisterExtensions
    {
        public static void ServicesRegister(this IServiceCollection services)
        {
            services.AddTransient<IAuthService,AuthService>();
            services.AddScoped<ITokenService,TokenService>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IRoleService,RoleService>();
        }
    }
}
