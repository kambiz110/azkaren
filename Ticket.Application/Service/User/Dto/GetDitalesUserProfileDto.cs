using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.User.Dto
{
   public class GetDitalesUserProfileDto
    {

        public string userId { get; set; }

        public long personId { get; set; }

        [Display(Name = "نام")]

        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
       public string LastName { get; set; }

        [Display(Name = "تلفن")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        [RegularExpression(@"(^09)?(\d{9,10})$", ErrorMessage = " {0}  را به درستی وارد نمائید")]
        public string Phone { get; set; }

        [Display(Name = "درجه")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public string darajeh { get; set; }

        [Display(Name = "محل خدمت")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public string yegan_r { get; set; }

        [Display(Name = "یگان خدمتی")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public string yegan { get; set; }
    }
}
