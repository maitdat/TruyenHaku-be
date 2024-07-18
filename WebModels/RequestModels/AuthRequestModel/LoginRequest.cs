using System.ComponentModel.DataAnnotations;
using WebCommon.Constants;

namespace WebModels.RequestModels.AuthRequestModel
{
    public class LoginRequest
    {
        public string UserName { get; set; }

        [Required(ErrorMessage = Constants.Commons.FIELD_REQUIRED)]
        public string Password { get; set; }
    }
}
