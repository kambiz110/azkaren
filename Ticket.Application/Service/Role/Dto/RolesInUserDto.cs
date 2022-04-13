using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Role.Dto
{
    public class RolesInUserDto
    {
        [Display(Name = "نام و نام خانوادگی")]

        public string FullName { get; set; }
        public long UserId { get; set; }
        [Display(Name = "نقش کاربری")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public GetShortRolesForShowAdmin getShortRoles { get; set; }

    }
}
