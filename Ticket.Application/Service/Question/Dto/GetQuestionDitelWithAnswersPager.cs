using Azmoon.Application.Service.Answer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Question.Dto
{
   public class GetQuestionDitelWithAnswersPager
    {
        public GetQuestionViewModel GetQuestionDto { get; set; }
        public GetAnswerWithPager AnswersWithPager { get; set; }
    }
}
