using Microsoft.AspNetCore.Identity;

namespace TruyenHakuModels.Entities
{
    public class UserAccount : IdentityUser
    {
        public string HoTen { get; set; }
        public DateTime NgaySinh {  get; set; }
    }
}
