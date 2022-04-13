using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Quiz.Dto
{
   public class GetQuizWithPeger
    {
        public PagerDto PagerDto { get; set; }

        public List<GetQuizDto> Quizes { get; set; }
    }
}
