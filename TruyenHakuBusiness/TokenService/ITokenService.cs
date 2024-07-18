using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruyenHakuModels.Entities;

namespace TruyenHakuBusiness.TokenService
{
    public interface ITokenService
    {
        Task<string> GenerateToken(UserAccount user);
    }
}
