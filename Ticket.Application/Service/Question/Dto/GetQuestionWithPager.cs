using Azmoon.Application.Service.Quiz.Dto;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Question.Dto
{
   public class GetQuestionWithPager
    {
        public PagerDto PagerDto { get; set; }

        public List<GetQuestionViewModel> Questiones { get; set; }
    }
}
