using Azmoon.Application.Service.Filter.Dto;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Quiz.Dto
{
   public class GetQuizStudentWithPeger
    {
        public PagerDto PagerDto { get; set; }

        public List<QuizAssignViewModel> Quizes { get; set; }
    }
}
