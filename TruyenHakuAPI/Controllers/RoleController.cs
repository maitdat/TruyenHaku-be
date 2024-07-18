using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TruyenHakuBusiness.RoleService;
using TruyenHakuCommon.Constants;
using TruyenHakuModels.RequestModels.RoleRequestModel;

namespace TruyenHakuAPI.Controllers
{
    [Route(Constants.Controller.DEFAULT_ROUTE_CONTROLLER)]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        
        public RoleController(IRoleService roleService) 
        {
            _roleService = roleService;
        }
        [Authorize(Roles ="Admin")]
        [HttpPut]
        public async Task<IActionResult> AddUserToRolesAsync(RoleRequestModel roleRequestModel)
        {
            var res =await _roleService.AddUserRoleAsync(roleRequestModel);
            if (res)
                return Ok();
             return BadRequest();
        }

    }
}