using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruyenHakuModels.ResponseModels
{
    public class BaseResponse
    {
        public bool Succeed { get; set; }
        public string Message { get; set; }
        public string[] Errors { get; set; }
    }
}
