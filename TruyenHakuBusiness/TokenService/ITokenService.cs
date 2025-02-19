using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TruyenHakuModels.Entities.Account;

namespace TruyenHakuBusiness.TokenService
{
    public interface ITokenService
    {
        Task<string> GenerateToken(UserAccount user);
        List<Claim> CreateClaimIdentity(UserAccount user, IList<string> roles);
        void SetTokenInsideCookie(string token, HttpContext httpContext);
    }
}
