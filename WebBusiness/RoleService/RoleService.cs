using Microsoft.AspNetCore.Identity;
using WebCommon.Constants;
using WebModels.Entities;
using WebModels.RequestModels.RoleRequestModel;

namespace WebBusiness.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<UserAccount> _userManager;
        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<UserAccount> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<bool> AddUserRoleAsync(RoleRequestModel roleRequest)
        {
            var user =await _userManager.FindByIdAsync(roleRequest.UserId);
            if (user == null)
            {
                throw new Exception(Constants.Commons.USER_NOT_EXIST);
            }

            foreach(var roleName in roleRequest.InputRoleName)
            {
                var role = await _roleManager.FindByNameAsync(roleName.Name);

                if (role == null)
                {
                    throw new Exception(string.Format(Constants.Commons.ITEM_NOT_EXIST, role));
                }

                await _userManager.AddToRoleAsync(user, role.Name);
            }

            return true;
        }

        public Task DeleteRoleAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateRoleAsync()
        {
            throw new NotImplementedException();
        }
    }
}
