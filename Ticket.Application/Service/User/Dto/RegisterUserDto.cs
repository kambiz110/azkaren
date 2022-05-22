using Azmoon.Common.Useful;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Azmoon.Application.Service.User.Dto
{
  public  class RegisterUserDto
    {
        public string Id { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        [RegularExpression(@"^(?:[a-zA-Z\s,=%$#&_\u0600-\u06FF\u0750-\u077F\u08A0-\u08FF\uFB50-\uFDCF\uFDF0-\uFDFF\uFE70-\uFEFF]|(?:\uD802[\uDE60-\uDE9F]|\uD83B[\uDE00-\uDEFF])){3,15}$", ErrorMessage = " {0}  را به فارسی وارد نمائید")]

        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        [RegularExpression(@"^(?:[a-zA-Z\s,=%$#&_\u0600-\u06FF\u0750-\u077F\u08A0-\u08FF\uFB50-\uFDCF\uFDF0-\uFDFF\uFE70-\uFEFF]|(?:\uD802[\uDE60-\uDE9F]|\uD83B[\uDE00-\uDEFF])){3,15}$", ErrorMessage = " {0}  را به فارسی وارد نمائید")]
        public string LastName { get; set; }

        [Display(Name = "نام پدر")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        [RegularExpression(@"^(?:[a-zA-Z\s,=%$#&_\u0600-\u06FF\u0750-\u077F\u08A0-\u08FF\uFB50-\uFDCF\uFDF0-\uFDFF\uFE70-\uFEFF]|(?:\uD802[\uDE60-\uDE9F]|\uD83B[\uDE00-\uDEFF])){3,15}$", ErrorMessage = " {0}  را به فارسی وارد نمائید")]
        public string name_father { get; set; }

        [Display(Name = "تلفن")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        [MobliPhon]
        public string Phone { get; set; }

        [Display(Name = "درجه/رتبه")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public int darajeh { get; set; }

        [Display(Name = "نوع درجه")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public int TypeDarajeh { get; set; }
       
        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public string tavalod { get; set; }


        [Display(Name = "شماره پرسنلی")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        [RegularExpression(@"(^40)?(\d{9}$)$", ErrorMessage = " {0}  را به درستی وارد نمائید")]

        public long personeli { get; set; }

        [CodeMelli(ErrorMessage = " {0}  را به درستی وارد نمائید ")]
        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
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


        [Display(Name = "محل خدمت")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public long WorkPlaceId { get; set; }

        public string WorkPlaceIdFake { get; set; } = "";

    }
}
