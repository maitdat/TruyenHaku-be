using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruyenHakuModels.RequestModels.AuthRequestModel;

namespace TruyenHakuBusiness.UserService
{
    public interface IUserService
    {
        public Task<UserModel> GetById(string id);
    }
}
