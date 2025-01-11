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
        public AppUserController(IAuthService authService, SignInManager<UserAccount> signInManager)
        {
            _authService = authService;
            _signInManager = signInManager;
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
        [HttpPost]
        public async Task<IActionResult> OnPost(string provider, string returnUrl = null)
        {
            // Kiểm tra yêu cầu dịch vụ provider tồn tại
            var listprovider = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var provider_process = listprovider.Find((m) => m.Name == provider);
            if (provider_process == null)
            {
                return NotFound("Dịch vụ không chính xác: " + provider);
            }

            // redirectUrl - là Url sẽ chuyển hướng đến - sau khi CallbackPath (/dang-nhap-tu-google) thi hành xong
            // nó bằng identity/account/externallogin?handler=Callback
            // tức là gọi OnGetCallbackAsync
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });

            // Cấu hình
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            // Chuyển hướng đến dịch vụ ngoài (Googe, Facebook)
            return new ChallengeResult(provider, properties);
        }
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
