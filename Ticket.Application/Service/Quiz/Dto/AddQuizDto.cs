using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Quiz.Dto
{
    public class AddQuizDto
    {
        public long Id { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public string Name { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public string Description { get; set; }


        [Display(Name = "زمان(دقیقه)")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public int? Timer { get; set; }

        [Display(Name = "تاریخ و زمان شروع")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "تاریخ و زمان پایان")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "رمز آزمون")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public string Password { get; set; }

        [Display(Name = "دسته بندی ")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public long GroupId { get; set; }

        [Display(Name = "بیشترین تعداد سوال قابل نمایش")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public int MaxQuestion { get; set; }

        [Display(Name = " آزمون دارای سطح دسترسی است ؟")]
        public bool IsPrivate { get; set; }

        public SelectListItem GroupSelectList { get; set; }
}
}
