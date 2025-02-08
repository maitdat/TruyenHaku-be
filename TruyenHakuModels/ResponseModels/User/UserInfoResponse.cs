using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruyenHakuModels.ResponseModels.User
{
    public class UserInfoResponse
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string? AvatarImg {  get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }    

    }
}
