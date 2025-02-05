using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruyenHakuModels.RequestModels.Auth
{
    public class ChangePasswordRequest
    {
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu mới")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu mới và Nhập lại mật khẩu mới không giống nhau.")]
        public string ConfirmPassword { get; set; }

        public string NewPasswordCheck { get; set; }
    }
}
