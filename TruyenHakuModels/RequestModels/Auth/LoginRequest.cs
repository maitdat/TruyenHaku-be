using System.ComponentModel.DataAnnotations;
using TruyenHakuCommon.Constants;

namespace TruyenHakuModels.RequestModels.AuthRequestModel
{
    public class LoginRequest
    {
        public string UserName { get; set; }

        [Required(ErrorMessage = Constants.Commons.FIELD_REQUIRED)]
        public string Password { get; set; }
    }
}
