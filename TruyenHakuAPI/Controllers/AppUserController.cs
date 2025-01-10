using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TruyenHakuBusiness.AuthService;
using TruyenHakuCommon.Constants;
using TruyenHakuModels.RequestModels.AuthRequestModel;

namespace TruyenHakuAPI.Controllers
{
    [Route(Constants.Controller.DEFAULT_ROUTE_CONTROLLER)]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AppUserController(IAuthService authService)
        {
            _authService = authService;
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
        [HttpGet("{userId}")]
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
