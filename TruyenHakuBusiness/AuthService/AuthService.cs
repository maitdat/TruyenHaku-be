using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using TruyenHakuBusiness.TokenService;
using TruyenHakuCommon.Constants;
using TruyenHakuModels;
using TruyenHakuModels.Entities.Account;
using TruyenHakuModels.RequestModels.Auth;
using TruyenHakuModels.RequestModels.AuthRequestModel;
using TruyenHakuModels.ResponseModels;
using TruyenHakuModels.ResponseModels.AuthModel;

namespace TruyenHakuBusiness.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<UserAccount> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContext;
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;
        public AuthService(UserManager<UserAccount> userManager, RoleManager<IdentityRole> roleManager, AppDbContext appDbContext, ITokenService tokenService, IHttpContextAccessor httpContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = appDbContext;
            _tokenService = tokenService;
            _httpContext = httpContext;
        }
        public async Task<LoginResponse> Login(LoginRequest userInfo)
        {
            var currentUser = await _userManager.FindByNameAsync(userInfo.UserName);
            if (currentUser == null)
            {
                throw new Exception(Constants.Commons.USER_NOT_EXIST);
            }
            if (await _userManager.CheckPasswordAsync(currentUser, userInfo.Password))
            {

                var res = new LoginResponse
                {
                    Message = "Đăng nhập thành công",
                    Token = await _tokenService.GenerateToken(currentUser),
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

        public async Task<ResponseToClient> ChangePassword(ChangePasswordRequest request)
        {
            var userClaim = _httpContext.HttpContext.User;
            var user = await _userManager.GetUserAsync(userClaim);
            var res = _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (res.IsCompletedSuccessfully)
            {
                return new ResponseToClient
                {
                    Succeed = true,
                };
            }
            return new ResponseToClient
            {
                Succeed = false,
            };
        }

        //public async Task<>

        public async Task<IdentityResult> Register(UserModel userInfo)
        {
            if (await _userManager.FindByNameAsync(userInfo.UserName) != null)
            {
                throw new Exception(Constants.Commons.USER_ALREADY_EXIST);
            }
            var newUser = new UserAccount
            {
                UserName = userInfo.UserName,
                Email = userInfo.Email,
                FullName = userInfo.HoTen,
            };
            var res = await _userManager.CreateAsync(newUser, userInfo.Password);
            return res;
        }

        public async Task<IList<string>> GetRoles(string userId)
        {
            var user =await _userManager.FindByIdAsync(userId);
            return await _userManager.GetRolesAsync(user);
        }

    }
}
