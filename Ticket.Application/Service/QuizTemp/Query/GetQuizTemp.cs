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

namespace Azmoon.Application.Service.QuizTemp.Query
{
  public  class GetQuizTemp : IGetQuizTemp
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public GetQuizTemp(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ResultDto<AddQuizTempDto> GetById(long id)
        {

            var quiztemp = _context.QuizStartTemps.AsNoTracking()
              .Where(p => p.Id == id )
              .FirstOrDefault();
            if (quiztemp == null)
            {
                return new ResultDto<AddQuizTempDto>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "موجود  نمی باشد"
                };

            }
            var result = _mapper.Map<AddQuizTempDto>(quiztemp);
            return new ResultDto<AddQuizTempDto>
            {
                Data = result,
                IsSuccess = true,
                Message = "موفق!"
            };
          
        }

        public ResultDto<AddQuizTempDto> GetByUserNameWithQuizId(long quizId, string username)
        {
            var quiz = _context.Quizzes.AsNoTracking()
             .Where(p => p.Id == quizId && p.Status == 1 && p.StartDate <= DateTime.Now && p.EndDate > DateTime.Now)
             .FirstOrDefault();
            if (quiz == null)
            {
                return new ResultDto<AddQuizTempDto>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "خطا در ارسال "
                };

            }
           
            if (!_context.QuizStartTemps.Where(p => p.QuizId == quizId && p.UserName == username && p.RegesterAt < quiz.StartDate).Any())
            {
                return new ResultDto<AddQuizTempDto>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "برای این آزمون در این بازه زمانی ثبت نشده است!"
                };
            }
            var quiztemp = _context.QuizStartTemps.AsNoTracking()
            .Where(p => p.QuizId == quizId && p.UserName == username && p.RegesterAt < quiz.StartDate)
            .FirstOrDefault();
            if (quiztemp == null)
            {
                return new ResultDto<AddQuizTempDto>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "موجود  نمی باشد"
                };

            }

            var result = _mapper.Map<AddQuizTempDto>(quiztemp);
            return new ResultDto<AddQuizTempDto>
            {
                Data = result,
                IsSuccess = true,
                Message = "موفق!"
            };

           
        }
    }
}
