using Azmoon.Application.Service.QuizTemp.Dto;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Interfaces.QuizTemp
{
   public interface IAddQuizStartTemp
    {
        ResultDto<AddQuizTempDto> Add(DateTime start , long quizId , string userName);
    }
}
