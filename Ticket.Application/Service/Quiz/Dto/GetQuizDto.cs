using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Quiz.Dto
{
   public class GetQuizDto
    {
        [Display(Name = "شماره آزمون")]
        public long Id { get; set; }
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "دسته بندی")]
        public string GroupName { get; set; }
        [Display(Name = "تاریخ و زمان شروع")]
        public DateTime StartDate { get; set; }

        [Display(Name = "تاریخ و زمان پایان")]
        public DateTime EndDate { get; set; }

        [Display(Name = "وضعیت")]
        public bool Status { get; set; }

        [Display(Name = "فیلتر")]
        public bool FilterStatus { get; set; }

        [Display(Name = "وضعیت")]
        public int state { get; set; }
    }
}
