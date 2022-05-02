using Azmoon.Application.Service.Quiz.Dto;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Interfaces.Quiz
{
   public interface IGetQuiz
    {
        ResultDto<GetQuizWithPeger> GetQuizes(int PageSize, int PageNo, string searchKey, int status, string username);
        ResultDto<AddQuizDto> GetQuizById(long id);
        ResultDto<GetQuizDetilesDto> GetQuizDetailsById(long id);
        ResultDto<GetQuizDetWithQuestionPager> GetQuizDetWithQuestionPagerById(long id);
        ResultDto<long> GetQuizIdByPasswordAsync(string password, long quizId);
       Task< AttemtedQuizViewModel> GetQuizByIdAsync(long id);
        ResultDto<GetQuizDetilesViewModel> GetQuizViewStartPageById(long id);
    }
}
