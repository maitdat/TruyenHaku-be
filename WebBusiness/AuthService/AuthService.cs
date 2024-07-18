using Microsoft.AspNetCore.Identity;
using WebBusiness.TokenService;
using WebCommon.Constants;
using WebModels;
using WebModels.Entities;
using WebModels.RequestModels.AuthRequestModel;
using WebModels.ResponseModels.AuthModel;

namespace WebBusiness.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<UserAccount> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;
        public AuthService(UserManager<UserAccount> userManager, RoleManager<IdentityRole> roleManager, AppDbContext appDbContext, ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = appDbContext;
            _tokenService = tokenService;
        }
        public async Task<LoginResponse> Login(LoginRequest userInfo)
        {
            var currentUser = await _userManager.FindByNameAsync(userInfo.UserName);
            if (currentUser == null)
            {
                throw new Exception(Constants.Commons.USER_NOT_EXIST);
            }
            if(await _userManager.CheckPasswordAsync(currentUser, userInfo.Password))
            {
                
                var res = new LoginResponse
                {
                    Message = "Đăng nhập thành công",
                    Token =await _tokenService.GenerateToken(currentUser),
                    IsSucceed = true
                };
                return res;
            }
            return new LoginResponse
            {
                Message = "Tài khoản hoặc mật khẩu không đúng",
                Token = "",
                IsSucceed = false
            };
        }

        public async Task<IdentityResult> Register(UserModel userInfo)
        {
            if(await _userManager.FindByNameAsync(userInfo.UserName) != null)
            {
                throw new Exception(Constants.Commons.USER_ALREADY_EXIST);
            }
            var newUser = new UserAccount
            {
                UserName = userInfo.UserName,
                Email = userInfo.Email,
                HoTen = userInfo.HoTen, 
            };
            var res = await _userManager.CreateAsync(newUser, userInfo.Password);
            return res;
        }

    }
}
