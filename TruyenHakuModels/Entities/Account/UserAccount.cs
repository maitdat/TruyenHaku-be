using Microsoft.AspNetCore.Identity;

namespace TruyenHakuModels.Entities.Account
{
    public class UserAccount : IdentityUser
    {
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
    }
}
