using AutoMapper;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Filter;
using Azmoon.Application.Service.Filter.Dto;
using Azmoon.Common.ResultDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Filter.Query
{
    public class GetFilter : IGetFilter
    {
        private readonly IDataBaseContext _context;

        public GetFilter(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<CreatFilterDto> GetByQuizId(long quizid)
        {
            var filter = _context.QuizFilters.AsNoTracking().Where(p => p.QuizId == quizid).Select(p =>
         new CreatFilterDto
         {
             TypeDarajeh = p.TypeDarajeh,
             UserList = p.UserNameOption,
             WorkPlaceId = TolongFirst(p.WorkpalceOption),
             WorkPlaceIdFake = _context.WorkPlaces.AsNoTracking().Where(d => d.Id == TolongFirst(p.WorkpalceOption)).FirstOrDefault().Name,
             WorkPlaceWithChildren = (ToBoolTwoChild(p.WorkpalceOption))
         })
            .FirstOrDefault();
            if (filter == null)
            {
                return new ResultDto<CreatFilterDto>
                {
                    IsSuccess = false
                };
            }
            return new ResultDto<CreatFilterDto>
            {
                Data = filter,
                IsSuccess = true,
                Message = "موفق"
            };
        }

        private long TolongFirst(string s)
        {
            var v = s.Split("_").ToArray();
            long number;

            bool isParsable = Int64.TryParse(v.ElementAt(0), out number);

            if (isParsable)
                return number;
            else
                return 0;

        }
        private bool ToBoolTwoChild(string s)
        {
            var v = s.Split("_").ToArray();
            int number;
            bool isParsable = Int32.TryParse(v.ElementAt(1), out number);
            if (number > 0)
            {
                return true;
            }
            return false;
        }
    }
}
