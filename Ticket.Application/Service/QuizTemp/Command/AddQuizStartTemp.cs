using AutoMapper;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.QuizTemp;
using Azmoon.Application.Service.QuizTemp.Dto;
using Azmoon.Common.ResultDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.QuizTemp.Command
{
   public class AddQuizStartTemp : IAddQuizStartTemp
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public AddQuizStartTemp(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ResultDto<AddQuizTempDto> Add(DateTime start, long quizId, string userName ,string ip)
        {
            var quiz = _context.Quizzes.AsNoTracking()
                .Where(p => p.Id == quizId && p.Status == 1 && p.StartDate <= DateTime.Now && p.EndDate > DateTime.Now)
                .FirstOrDefault();
            if (quiz==null)
            {
                return new ResultDto<AddQuizTempDto> {
                Data=null,
                IsSuccess=false,
                Message="آزمون فعال نمی باشد"
                };

            }
            if (_context.QuizStartTemps.Where(p=>p.QuizId==quizId && p.UserName==userName && p.RegesterAt< quiz.StartDate).Any())
            {
                return new ResultDto<AddQuizTempDto>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "برای این آزمون در این بازه زمانی ثبت شده است!"
                };
            }
 
            var temp = new Domain.Entities.QuizStartTemp
            {
                Ip=ip,
                UserName = userName,
                QuizId = quizId,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMinutes(Convert.ToDouble(quiz.Timer))

            };
            _context.QuizStartTemps.Add(temp);
          var saved=  _context.SaveChanges();
            if (saved>0)
            {
                var result = _mapper.Map<AddQuizTempDto>(temp);
                return new ResultDto<AddQuizTempDto>
                {
                    Data = result,
                    IsSuccess = true,
                    Message = "موفق"
                };
            }
            return new ResultDto<AddQuizTempDto>
            {
                Data = null,
                IsSuccess = false,
                Message = "عملیات نا موفق!"
            };
        }
    }
}
