using TruyenHakuBusiness.ApplicationService.CrawlDataService;
using TruyenHakuBusiness.ApplicationService.MangaService;
using TruyenHakuBusiness.AuthService;
using TruyenHakuBusiness.CommonService;
using TruyenHakuBusiness.Repository;
using TruyenHakuBusiness.RoleService;
using TruyenHakuBusiness.TokenService;
using TruyenHakuBusiness.UnitOfWork;
using TruyenHakuBusiness.UserService;

namespace TruyenHakuAPI.Extensions
{
    public static class ServicesRegisterExtensions
    {
        public static void ServicesRegister(this IServiceCollection services)
        {
            services.AddTransient<IAuthService,AuthService>(); 
            services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddTransient<IUnitofWork, UnitOfWork>();

            services.AddScoped<ITokenService,TokenService>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IRoleService,RoleService>();
            services.AddScoped<IMangaService,MangaService>();
            services.AddScoped<ICommonService,CommonService>();
            services.AddScoped<ICrawlDataService,CrawlDataService>();
        }
    }
}
