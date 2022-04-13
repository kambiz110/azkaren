using Azmoon.Application.Service.Result.Dto;
using Azmoon.Application.Service.Result.Query;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Interfaces.Result
{
   public interface IGetResultQuiz
    {
        ResultDto<GetResutQuizWithPager> getResultWithPager(int PageSize, int PageNo, string searchKey, int status, long guizId);
        ResultDto<GetResutQuizWithPager> getResultByUserId(int PageSize, int PageNo,  int status, string UserId);
        ResultDto<AddResultQuizDto> getResultByQuizId(long id);
    }
}
