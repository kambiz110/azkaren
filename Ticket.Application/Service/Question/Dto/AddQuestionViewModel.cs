using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Question.Dto
{
   public class AddQuestionViewModel
    {
        public long Id { get; set; }
        [Display(Name = "متن")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public string Text { get; set; }
        public long QuizId { get; set; }
        public string Ip { get; set; }

    }
}
