using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruyenHakuModels.RequestModels.RoleRequestModel;
using static TruyenHakuCommon.Enums;

namespace TruyenHakuBusiness.RoleService
{
    public interface IRoleService
    {
        public Task<bool> AddUserRoleAsync(RoleRequestModel roleRequest);
        public Task UpdateRoleAsync();
        public Task DeleteRoleAsync();
    }
}
