using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.User.Dto
{
   public class ShortRegisterDto
    {
        [Display(Name = "تلفن")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        [RegularExpression(@"(^09)?(\d{10})$", ErrorMessage = " {0}  را به درستی وارد نمائید")]
        public string Phone { get; set; }
        [Display(Name = "شماره پرسنلی")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        [RegularExpression(@"(^40)?(\d{9}$)$", ErrorMessage = " {0}  را به درستی وارد نمائید")]

        public long personeli { get; set; }
        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        [RegularExpression(@"^[0-9]{8,10}$", ErrorMessage = " {0}  را به درستی وارد نمائید")]
        public string melli { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*_=+-]).{8,12}$", ErrorMessage = "رمز باید شامل 8 کارکتر حروف بزرگ و عدد  و متا کارکتر باشد")]
        public string Password { get; set; }
        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*_=+-]).{8,12}$", ErrorMessage = "رمز باید شامل 8 کارکتر حروف بزرگ و عدد  و متا کارکتر باشد")]
        [Compare(nameof(Password), ErrorMessage = "پسورد مطابق با رمز قبلی نمی باشد")]
        public string RePassword { get; set; }
    }
}
