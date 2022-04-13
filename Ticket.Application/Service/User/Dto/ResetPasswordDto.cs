using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.User.Dto
{
    public class ResetPasswordDto
    {
        [Display(Name = "نام کاربری")]
        public string FullName { get; set; }

        public string UserName { get; set; }

        [Display(Name = "رمز عبور جدید")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public string Password { get; set; }
    }
}
