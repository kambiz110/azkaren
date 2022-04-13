using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Azmoon.Application.Interfaces.Answer;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Service.Answer.Dto;
using Azmoon.Common.Pagination;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Answer.Query
{
   public class GetAnswer : IGetAnswer
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public GetAnswer(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ResultDto<AddAnswerDto> GetById(long id)
        {
            var answer = _context.Answers.Where(p => p.Id == id && p.Status>0)
             .AsNoTracking()
             .FirstOrDefault();
            var model = _mapper.Map<AddAnswerDto>(answer);
            if (model != null)
            {
                return new ResultDto<AddAnswerDto>
                {
                    Data = model,
                    IsSuccess = true,
                    Message = "موفق"
                };
            }
            return new ResultDto<AddAnswerDto>
            {
                Data = null,
                IsSuccess = false,
                Message = "نا موفق"
            };
        }

        public ResultDto<GetAnswerWithPager> GetByQuestionId(int PageSize, int PageNo, long questionId)
        {
            int rowCount = 0;
            var answeres = _context.Answers.Where(p => p.QuestionId == questionId && p.Status >0)
            .AsNoTracking()
            .AsQueryable();

            var result = new GetAnswerWithPager() { };
            if (answeres != null)
            {

                var paging = answeres.ToPaged(PageNo, PageSize, out rowCount).ToList();
                var model = _mapper.Map<List<GetAnswerDto>>(paging);
                result.PagerDto = new PagerDto
                {
                    PageNo = PageNo,
                    PageSize = PageSize,
                    TotalRecords = rowCount
                };
                result.getAnswers = model;
                return new ResultDto<GetAnswerWithPager>
                {
                    Data = result,
                    IsSuccess = true,
                    Message = "موفق"
                };
            }
            return new ResultDto<GetAnswerWithPager>
            {
                Data = null,
                IsSuccess = false,
                Message = "ناموفق"
            };
        }
    }
}
