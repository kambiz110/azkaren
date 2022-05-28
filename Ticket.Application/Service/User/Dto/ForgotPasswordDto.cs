using Azmoon.Common.Useful;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.User.Dto
{
   public class ForgotPasswordDto
    {
        [Display(Name = "نام پدر")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        [RegularExpression(@"^(?:[a-zA-Z\s,=%$#&_\u0600-\u06FF\u0750-\u077F\u08A0-\u08FF\uFB50-\uFDCF\uFDF0-\uFDFF\uFE70-\uFEFF]|(?:\uD802[\uDE60-\uDE9F]|\uD83B[\uDE00-\uDEFF])){3,15}$", ErrorMessage = " {0}  را به فارسی وارد نمائید")]
        public string name_father { get; set; }
        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        [MobliPhon]
        public string Phone { get; set; }

        [Display(Name = "شماره پرسنلی")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        [RegularExpression(@"(^40)?(\d{9}$)$", ErrorMessage = " {0}  را به درستی وارد نمائید")]

        public long personeli { get; set; }

        [CodeMelli(ErrorMessage = " {0}  را به درستی وارد نمائید ")]
        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public string melli { get; set; }

        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public string tavalod { get; set; }
    }
}
