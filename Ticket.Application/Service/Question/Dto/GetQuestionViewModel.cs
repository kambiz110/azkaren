using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Question.Dto
{
  public class GetQuestionViewModel
    {

        public long Id { get; set; }
        [Display(Name = "متن سوال")]
        public string Text { get; set; }

        public long QuizId { get; set; }

        [Display(Name = "وضعیت")]
        public bool Status { get; set; }

        [Display(Name = "تعداد جواب صحیح ")]
        public int CountTrueAsnswer { get; set; }
        [Display(Name = "تعداد سوال اشتباه ")]
        public int CountFalseAsnswer { get; set; }
    }
}
