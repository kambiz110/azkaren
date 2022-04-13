using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.User.Dto
{
    public class UserShowAdminDto
    {
        public string Id { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public string LastName { get; set; }
        [Display(Name = "شماره پرسنلی")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]

        public string UserName { get; set; }
        [Display(Name = "تلفن")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public string Phone { get; set; }


        [Display(Name = "عکس")]

        public string Image { get; set; }

        public bool LockoutEnabled { get; set; }

    }
}
