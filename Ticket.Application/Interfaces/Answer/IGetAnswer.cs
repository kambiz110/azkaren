using Azmoon.Application.Service.Answer.Dto;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Interfaces.Answer
{
   public interface IGetAnswer
    {
        ResultDto<GetAnswerWithPager> GetByQuestionId(int PageSize, int PageNo, long questionId);
        ResultDto<AddAnswerDto> GetById( long id);
    }
}
