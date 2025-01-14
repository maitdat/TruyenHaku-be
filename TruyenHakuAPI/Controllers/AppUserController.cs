using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TruyenHakuBusiness.AuthService;
using TruyenHakuCommon.Constants;
using TruyenHakuModels.Entities.Account;
using TruyenHakuModels.RequestModels.AuthRequestModel;

namespace TruyenHakuAPI.Controllers
{
    [Route(Constants.Controller.DEFAULT_ROUTE_CONTROLLER)]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly SignInManager<UserAccount> _signInManager;
        private readonly UserManager<UserAccount> _userManager;
        public AppUserController(IAuthService authService, SignInManager<UserAccount> signInManager, UserManager<UserAccount> userManager)
        {
            _authService = authService;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        [HttpPost]
        public async Task<IActionResult> Register(UserModel userinfo)
        {
            var res = await _authService.Register(userinfo);
            if(res.Succeeded)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest(res);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest userinfo)
        {
            var res = await _authService.Login(userinfo);
            if (res.IsSucceed)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest(res);
            }
        }
        [HttpGet]
        public async Task<IActionResult> LoginThirdParty([FromQuery] ExternalLoginRequest request)
        {
            try
            {
                var listprovider = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                var providerProcess = listprovider.Find((m) => m.Name.Equals(request.Provider, StringComparison.OrdinalIgnoreCase));

                if (providerProcess == null)
                {
                    return BadRequest(new { Message = "Dịch vụ không chính xác: " + request.Provider });
                }

                // Tạo URL callback (URL sẽ được gọi sau khi xác thực)
                var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "AppUser", new { returnUrl = request.ReturnUrl }, Request.Scheme);

                // Cấu hình thông tin xác thực
                var properties = _signInManager.ConfigureExternalAuthenticationProperties(request.Provider, redirectUrl);

                // Chuyển hướng người dùng đến dịch vụ bên ngoài (Google/Facebook)
                return new ChallengeResult(request.Provider, properties);  // This initiates the external login
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            // Kiểm tra yêu cầu dịch vụ provider tồn tại
            
        }


        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null)
        {
            try
            {
                var loginInfo = await _signInManager.GetExternalLoginInfoAsync();

                if (loginInfo == null)
                {
                    return BadRequest(new { Message = "Không thể lấy thông tin đăng nhập từ provider." });
                }

                // Đăng nhập hoặc tạo mới tài khoản nếu cần
                var result = await _signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, isPersistent: false);

                if (!result.Succeeded)
                {
                    // Nếu không thành công, chuyển đến luồng đăng ký hoặc thông báo lỗi
                    return BadRequest(new { Message = "Đăng nhập thất bại." });
                }

                // Trả về token hoặc thông tin người dùng
                var user = await _userManager.FindByLoginAsync(loginInfo.LoginProvider, loginInfo.ProviderKey);
                var token = GenerateJwtToken(user);

                return Ok(new
                {
                    Token = token,
                    ReturnUrl = returnUrl ?? "/"
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            // Lấy thông tin đăng nhập từ provider
            
        }

        // Hàm giả lập tạo token
        private string GenerateJwtToken(IdentityUser user)
        {
            // Tạo JWT token
            return "fake-jwt-token";
        }


        // Model yêu cầu từ client
        
        [HttpGet]
        public async Task<IActionResult> GetRoles(string userId)
        {
            var res = await _authService.GetRoles(userId);
            return Ok(res);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> TestAdmin()
        {
            var x = 1 + 1;
            return Ok(x);
        }

        [Authorize]
        [HttpGet]
        public bool TestAuthorize()
        {
            return true;
        }

    }
}
