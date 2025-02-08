using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruyenHakuModels.RequestModels.AuthRequestModel;
using TruyenHakuModels.ResponseModels.User;

namespace TruyenHakuBusiness.UserService
{
    public interface IUserService
    {
        public Task<UserInfoResponse> GetById(string id);
        Task<UserInfoResponse> GetCurrentUser();
    }
}
