using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Answer.Dto
{
  public  class GetAnswerWithPager
    {
        public PagerDto PagerDto { get; set; }
        public List<GetAnswerDto> getAnswers { get; set; }
    }
}
