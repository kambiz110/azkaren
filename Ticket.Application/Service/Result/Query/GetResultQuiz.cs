using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Result;
using Azmoon.Application.Service.Result.Dto;
using Azmoon.Common.Pagination;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Result.Query
{
    public class GetResultQuiz : IGetResultQuiz
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public GetResultQuiz(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ResultDto<AddResultQuizDto> getResultByQuizId(long id)
        {
            var model = _context.Results.Where(p => p.Status == 1 && p.Id == id)
      .AsNoTracking()
      .FirstOrDefault();
            if (model != null)
            {
                var result = _mapper.Map<AddResultQuizDto>(model);

                return new ResultDto<AddResultQuizDto>
                {

                    Data = result,
                    IsSuccess = true,
                    Message = "Success"
                };
            }
            return new ResultDto<AddResultQuizDto>
            {
                Data = null,
                IsSuccess = false,
                Message = "Warninge"
            };
        }

        public ResultDto<GetResutQuizWithPager> getResultByUserId( int PageNo, int PageSize, int status, string UserId)
        {
            var date = new GetResutQuizWithPager() { };
            int rowCount = 0;
            var model = _context.Results.Where(p => p.Status == status )
                .AsNoTracking()
                .Include(p => p.Student)
                .Include(p=>p.Quiz)
                .AsQueryable();
            if (!String.IsNullOrEmpty(UserId))
            {
                model = model.Where(p => p.StudentId == UserId.Trim() )

               .AsQueryable();
            }


            if (model != null)
            {
                
                var paging = model.ToPaged(PageNo, PageSize, out rowCount).ToList();
                var result = _mapper.Map<List<GetResutQuizDto>>(paging);
                
                date.ResultQuizDtos = result;
                date.PagerDto = new PagerDto
                {
                    PageNo = PageNo,
                    PageSize = PageSize,
                    TotalRecords = rowCount
                };
                return new ResultDto<GetResutQuizWithPager>
                {

                    Data = date,
                    IsSuccess = true,
                    Message = "Success"
                };
            }

            return new ResultDto<GetResutQuizWithPager>
            {
                Data = null,
                IsSuccess = false,
                Message = "Warninge"
            };
        }

        public ResultDto<GetResutQuizWithPager> getResultWithPager(int PageSize, int PageNo, string searchKey, int status, long guizId)
        {
            var date = new GetResutQuizWithPager() { };
            int rowCount = 0;
            var model = _context.Results.Where(p => p.Status == status)
                .AsNoTracking()
                .Include(p=>p.Student)
                .AsQueryable();
            if (!String.IsNullOrEmpty(searchKey))
            {
                model = model.Where(p => p.Student.FirstName.Contains(searchKey.Trim()) ||
                p.Student.LastName.Contains(searchKey.Trim()) ||
                  p.Student.UserName.Contains(searchKey.Trim())
                ).AsQueryable();
            }
        

            if (model != null)
            {
                var result = _mapper.Map<List<GetResutQuizDto>>(model);
                var paging = result.ToPaged(PageNo, PageSize, out rowCount).ToList();
                date.ResultQuizDtos = paging;
                date.PagerDto = new PagerDto
                {
                    PageNo = PageNo,
                    PageSize = PageSize,
                    TotalRecords = rowCount
                };
                return new ResultDto<GetResutQuizWithPager>
                {

                    Data = date,
                    IsSuccess = true,
                    Message = "Success"
                };
            }

            return new ResultDto<GetResutQuizWithPager>
            {
                Data = null,
                IsSuccess = false,
                Message = "Warninge"
            };
        }
    }
}
