using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TruyenHakuBusiness.AuthService;
using TruyenHakuBusiness.TokenService;
using TruyenHakuBusiness.UserService;
using TruyenHakuCommon.Constants;
using TruyenHakuModels.Entities.Account;
using TruyenHakuModels.RequestModels.AuthRequestModel;

namespace TruyenHakuAPI.Controllers
{
    [Route(Constants.Controller.DEFAULT_ROUTE_CONTROLLER)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        public UserController(
            IAuthService authService,
            IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest userinfo)
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

        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var res = await _userService.GetCurrentUser();
            return Ok(res);
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
