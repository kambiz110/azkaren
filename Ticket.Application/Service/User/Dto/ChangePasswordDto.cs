using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.User.Dto
{
    public class ChangePasswordDto
    {

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public string UserName { get; set; }

        [Display(Name = "رمز عبور قدیمی")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "رمز عبور جدید")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "تکرار رمز عبور ")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = " تکرار با رمز وارد شده مطابقت ندارد ")]
        public string RePassword { get; set; }

        public string ReturnUrl { get; set; }
    }
}
