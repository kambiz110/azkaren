using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Result.Dto
{
   public class GetResutQuizDto
    {
        public long Id { get; set; }

        [Display(Name = "نام آزمون")]
        public string QuizName { get; set; }

        [Display(Name = "نمره")]
        public int Points { get; set; }
        [Display(Name = "تعداد سوال")]
        public int MaxPoints { get; set; }

        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [Display(Name = "شماره موبایل")]
        public string PhoneNumber { get; set; }

        public string StudentId { get; set; }
        [Display(Name = "تاریخ شرکت در آزمون")]
        public DateTime StartQuiz { get; set; }
        [Display(Name = "زمان پایان آزمون")]
        public DateTime? EndQuiz { get; set; }
        public long QuizId { get; set; }
    }
}
