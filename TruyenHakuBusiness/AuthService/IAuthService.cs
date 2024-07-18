using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruyenHakuModels.RequestModels.AuthRequestModel;
using TruyenHakuModels.ResponseModels.AuthModel;

namespace TruyenHakuBusiness.AuthService
{
    public interface IAuthService
    {
        public Task<IdentityResult> Register (UserModel userInfo);
        public Task<LoginResponse> Login (LoginRequest userInfo);
    }
}
