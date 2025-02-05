using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruyenHakuCommon;

namespace TruyenHakuModels.Entities.Account
{
    public class OAuthToken : BaseEntityCommon
    {
        public string AuthToken { get; set; }
        int ExpirationTime { get; set; }
    }
}
