using Azmoon.Application.Service.Question.Dto;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Interfaces.Question
{
   public interface IGetQuestion
    {
        ResultDto<AddQuestionViewModel> GetById(long Id);
        ResultDto<GetQuestionDitelWithAnswersPager> GetQuestionDitelWithAnswersPager(long questionId);
        ResultDto<List<GetQuestionViewModel>> GetByUserName(string userName);
        ResultDto<GetQuestionWithPager> GetByQuizId(int PageSize, int PageNo, long QuizId);
    }
}
