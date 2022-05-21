using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Common.ResultDto;
using Azmoon.Common.Useful;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Result.Query
{

    public interface IAutorizResultInDb
    {
        ResultDto autorizationResultDb(long resultQuizId);
    }
    public class AutorizResultInDb : IAutorizResultInDb
    {
        private readonly IDataBaseContext _context;


        public AutorizResultInDb(IDataBaseContext context)
        {
            _context = context;
    
        }

        public ResultDto autorizationResultDb(long resultQuizId)
        {
            var resultQuiz = _context.Results.AsNoTracking().Where(p => p.Id == resultQuizId&& p.Status==1).FirstOrDefault();

            if (resultQuiz!=null)
            {
                var user = _context.Users.AsNoTracking().Where(p=>p.Id== resultQuiz.StudentId).FirstOrDefault();

               
                var result = resultQuiz.AnsweresInQuiz.ToEncodeAndHashMD5ForResultQuiz(user.UserName+ resultQuiz.QuizId.ToString()+ resultQuiz.Points.ToString());

                

                if (result== resultQuiz.AuthorizationResult)
                {
                    return new ResultDto { 
                    IsSuccess=true,
                    Message="اعتبارسنجی صحیح است:)"
                    };
                }
            }
           

            return new ResultDto {
                IsSuccess = false,
                Message = "اعتبارسنجی غلط است:("
            };

        }
    }
}
