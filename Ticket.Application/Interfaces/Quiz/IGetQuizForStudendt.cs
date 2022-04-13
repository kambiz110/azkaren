using Azmoon.Application.Service.Quiz.Dto;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Interfaces.Quiz
{
   public interface IGetQuizForStudendt
    {
        ResultDto<GetQuizForStudendtDto> Exequte(long QuizId);
    }
}
