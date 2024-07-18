using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModels.ResponseModels.AuthModel
{
    public class LoginResponse
    {
        public string Message { get; set; }
        public string Token { get; set; }
        public bool IsSucceed { get; set; }
    }
}
