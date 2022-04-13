using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Quiz.Dto
{
   public class GetQuizDetilesDto
    {
        public long Id { get; set; }
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "تاریخ و زمان شروع")]
        public DateTime StartDate { get; set; }

        [Display(Name = "تاریخ و زمان پایان")]
        public DateTime EndDate { get; set; }

        [Display(Name = "وضعیت")]
        public bool Status { get; set; }

        [Display(Name = "زمان(دقیقه)")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public int Timer { get; set; }

        [Display(Name = "دسته بندی ")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]

        public string CategoreName { get; set; }

        [Display(Name = "بیشترین تعداد سوال قابل نمایش")]
        public int MaxQuestion { get; set; }

    }
}
