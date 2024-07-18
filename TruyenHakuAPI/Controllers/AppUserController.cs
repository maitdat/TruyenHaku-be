using Microsoft.AspNetCore.Mvc;
using WebAPI.CustomAttribute;
using WebBusiness.AuthService;
using WebCommon.Constants;
using WebModels.RequestModels.AuthRequestModel;

namespace WebAPI.Controllers
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
        [Authorize("Admin")]
        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var x = 1 + 1;
            return Ok(x);
        }

    }
}
