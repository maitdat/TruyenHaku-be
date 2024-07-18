using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.RequestModels.AuthRequestModel;

namespace WebBusiness.UserService
{
    public interface IUserService
    {
        public Task<UserModel> GetById(string id);
    }
}
