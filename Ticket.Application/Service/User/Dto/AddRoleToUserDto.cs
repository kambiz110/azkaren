using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.User.Dto
{
    public class AddRoleToUserDto
    {

        public string Id { get; set; }



        [Display(Name = "نام و نام خانوادگی")]

        public string FullName { get; set; }

        public List<GetRoleDto> Roles { get; set; }
        [Display(Name = "نقش کاربری")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public string Role { get; set; }

    }
}
