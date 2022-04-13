using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.User.Dto
{
  public class ResetPasswordStudendtDto
    {
        [Display(Name = "رمز عبور قدیمی")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public string OldPassword { get; set; }
        [Display(Name = "رمز عبور جدید")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*_=+-]).{8,12}$", ErrorMessage = "رمز باید شامل 8 کارکتر حروف بزرگ و عدد  و متا کارکتر باشد")]
        public string Password { get; set; }
        [Display(Name = "تکرار رمز عبور جدید")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        [Compare(nameof(Password), ErrorMessage = "پسورد مطابق با رمز قبلی نمی باشد")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*_=+-]).{8,12}$", ErrorMessage = "رمز باید شامل 8 کارکتر حروف بزرگ و عدد  و متا کارکتر باشد")]
        public string RePassword { get; set; }

    }
}
