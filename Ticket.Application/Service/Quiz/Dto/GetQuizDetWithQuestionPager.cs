using Azmoon.Application.Service.Question.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Quiz.Dto
{
   public class GetQuizDetWithQuestionPager
    {
        public GetQuestionWithPager getQuestionWithPager { get; set; }
        public GetQuizDetilesDto GetQuizDetiles { get; set; }
    }
}
