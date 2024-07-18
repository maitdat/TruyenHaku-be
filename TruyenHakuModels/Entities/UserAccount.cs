using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCommon;

namespace WebModels.Entities
{
    public class UserAccount : IdentityUser
    {
        public string HoTen { get; set; }
        public DateTime NgaySinh {  get; set; }
    }
}
