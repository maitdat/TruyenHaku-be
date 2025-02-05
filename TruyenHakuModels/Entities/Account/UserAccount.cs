using Microsoft.AspNetCore.Identity;

namespace TruyenHakuModels.Entities.Account
{
    public class UserAccount : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
