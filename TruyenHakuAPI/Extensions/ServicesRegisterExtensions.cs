using TruyenHakuBusiness.AuthService;
using TruyenHakuBusiness.RoleService;
using TruyenHakuBusiness.TokenService;
using TruyenHakuBusiness.UserService;

namespace TruyenHakuAPI.Extensions
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
