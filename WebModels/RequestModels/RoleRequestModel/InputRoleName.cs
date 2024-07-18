using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModels.RequestModels.RoleRequestModel
{
    public class InputRoleName
    {
        [Required(ErrorMessage = "Phải nhập tên role")]
        [Display(Name = "Tên của Role")]
        [StringLength(100, ErrorMessage = "{0} dài {2} đến {1} ký tự.", MinimumLength = 3)]
        public string Name { get; set; }
    }
}
