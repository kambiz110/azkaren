using Azmoon.Application.Service.QuizTemp.Dto;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Interfaces.QuizTemp
{
   public interface IGetQuizTemp
    {
        ResultDto<AddQuizTempDto> GetById(long id);
        ResultDto<AddQuizTempDto> GetByUserNameWithQuizId(long quizId , string username);
    }
}
