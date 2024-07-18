using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.RequestModels.AuthRequestModel;
using WebModels.ResponseModels.AuthModel;

namespace WebBusiness.AuthService
{
    public interface IAuthService
    {
        public Task<IdentityResult> Register (UserModel userInfo);
        public Task<LoginResponse> Login (LoginRequest userInfo);
    }
}
