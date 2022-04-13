using Azmoon.Application.Service.Result.Dto;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Result.Query
{
  public  class GetResutQuizWithPager
    {
        public PagerDto PagerDto { get; set; }
       public List<GetResutQuizDto> ResultQuizDtos { get; set; }
    }
}
