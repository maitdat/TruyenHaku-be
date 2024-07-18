using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.Entities;

namespace WebBusiness.TokenService
{
    public interface ITokenService
    {
        Task<string> GenerateToken(UserAccount user);
    }
}
