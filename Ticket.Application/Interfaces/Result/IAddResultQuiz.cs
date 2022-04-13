using Azmoon.Application.Service.Result.Dto;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Interfaces.Result
{
  public  interface IAddResultQuiz
    {
        ResultDto<AddResultQuizDto>addResultQuiz(AnswerQuizStudendt dto);
    }
}
