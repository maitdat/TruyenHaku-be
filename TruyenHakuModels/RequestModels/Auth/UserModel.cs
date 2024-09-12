using System.ComponentModel.DataAnnotations;
using TruyenHakuCommon.Constants;

namespace TruyenHakuModels.RequestModels.AuthRequestModel
{
    public class UserModel
    {
        [EmailAddress]
        [Required(ErrorMessage = Constants.Commons.EMAIL_INVALID)]
        public string Email { get; set; }

        [Required(ErrorMessage = Constants.Commons.FIELD_REQUIRED)]
        public string UserName { get; set; }  

        [Required(ErrorMessage = Constants.Commons.FIELD_REQUIRED)]
        public string Password { get; set; }
        
        public string HoTen { get; set; }
        public string SDT { get; set; }

    }
}
