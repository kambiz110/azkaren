using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Filter;
using Azmoon.Application.Service.Filter.Dto;
using Azmoon.Common.ResultDto;
using Azmoon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Filter.Command
{
    public class AddQuizFilter : IAddQuizFilter
    {
        private readonly IDataBaseContext _context;

        public AddQuizFilter(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto AddFilter(CreatFilterDto dto)
        {
            var filter = new QuizFilter() {

                QuizId = dto.QuizId,
                TypeDarajeh = dto.TypeDarajeh,
                UserNameOption = dto.UserList,
                WorkpalceOption = dto.WorkPlaceId + "_" + (dto.WorkPlaceWithChildren==true?1:0).ToString()
            };
            var quizFilter = _context.QuizFilters.AsNoTracking().Where(p => p.QuizId == dto.QuizId).FirstOrDefault();
            if (quizFilter!=null)
            {
                filter.Id = quizFilter.Id;
               
            }
            _context.QuizFilters.Add(filter);
            var saved = _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = saved>0?true:false,
                Message = saved > 0 ? "موفق" : "نا موفق"
            };
           
        }
    }
}
