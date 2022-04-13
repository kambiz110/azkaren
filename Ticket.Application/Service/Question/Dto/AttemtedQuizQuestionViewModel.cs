using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Question.Dto
{
   public class AttemtedQuizQuestionViewModel
    {
        public AttemtedQuizQuestionViewModel(IList<AttemtedQuizAnswerViewModel> answers)
        {
            this.Answers = new List<AttemtedQuizAnswerViewModel>(); ;
        }
        public AttemtedQuizQuestionViewModel()
        {

        }
        public long Id { get; set; }
        [Display(Name = "متن سوال")]
        public string Text { get; set; }

        public IList<AttemtedQuizAnswerViewModel> Answers { get; set; }
    }
}
