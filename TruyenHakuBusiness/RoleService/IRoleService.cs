using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.RequestModels.RoleRequestModel;
using static WebCommon.Enums;

namespace WebBusiness.RoleService
{
    public interface IRoleService
    {
        public Task<bool> AddUserRoleAsync(RoleRequestModel roleRequest);
        public Task UpdateRoleAsync();
        public Task DeleteRoleAsync();
    }
}
