using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Question;
using Azmoon.Application.Service.Question.Dto;
using Azmoon.Common.ResultDto;
using Azmoon.Common.Pagination;

namespace Azmoon.Application.Service.Question.Query
{
    public class GetQuestion : IGetQuestion
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public GetQuestion(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// استخراج سوالات برای انجام و توزیع
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="userAnswerId"></param>
        /// <param name="LoginUserName"></param>
        /// <returns></returns>


        public ResultDto<AddQuestionViewModel> GetById(long Id)
        {
            var questione = _context.Qestions.Where(p => p.Id == Id)
               .AsNoTracking()
               .FirstOrDefault();
            var model = _mapper.Map<AddQuestionViewModel>(questione);
            if (model != null)
            {
                return new ResultDto<AddQuestionViewModel>
                {
                    Data = model,
                    IsSuccess = true,
                    Message = ""
                };
            }
            return new ResultDto<AddQuestionViewModel>
            {
                Data = null,
                IsSuccess = false,
                Message = ""
            };
        }

        public ResultDto<GetQuestionWithPager> GetByQuizId(int PageSize, int PageNo, long QuizId)
        {
            int rowCount = 0;
            var questiones = _context.Qestions.Where(p => p.QuizId == QuizId && p.Status == 1)
                .Include(p=>p.Answers)
            .AsNoTracking()
            .ToList();
            var modell = new GetQuestionWithPager() { };
            if (questiones != null)
            {

                var paging = questiones.ToPaged(PageNo, PageSize, out rowCount).ToList();
                var model = _mapper.Map<List<GetQuestionViewModel>>(paging);
                foreach (var item in paging)
                {
                 var countIsTrue =   item.Answers.Where(p => p.IsTrue == true).Count();
                    var countIsFalse = item.Answers.Where(p => p.IsTrue == false).Count();
                    model.Where(p => p.Id == item.Id).FirstOrDefault().CountTrueAsnswer = countIsTrue;
                    model.Where(p => p.Id == item.Id).FirstOrDefault().CountFalseAsnswer = countIsFalse;
                }
                modell.PagerDto = new PagerDto
                {
                    PageNo = PageNo,
                    PageSize = PageSize,
                    TotalRecords = rowCount
                };
                modell.Questiones = model;
                return new ResultDto<GetQuestionWithPager>
                {
                    Data = modell,
                    IsSuccess = true,
                    Message = ""
                };
            }
            return new ResultDto<GetQuestionWithPager>
            {
                Data = null,
                IsSuccess = false,
                Message = ""
            };
        }

        public ResultDto<List<GetQuestionViewModel>> GetByUserName(string userName)
        {
            var UserQuestion = _context.Users.Where(p => p.UserName == userName).AsNoTracking()
               .FirstOrDefault();
            var questiones = _context.Qestions
               .AsNoTracking()
               .ToList();
            var model = _mapper.Map<List<GetQuestionViewModel>>(questiones);
            if (model != null)
            {
                return new ResultDto<List<GetQuestionViewModel>>
                {
                    Data = model,
                    IsSuccess = true,
                    Message = ""
                };
            }
            return new ResultDto<List<GetQuestionViewModel>>
            {
                Data = null,
                IsSuccess = false,
                Message = ""
            };
        }

        public ResultDto<GetQuestionDitelWithAnswersPager> GetQuestionDitelWithAnswersPager(long questionId)
        {
            var questione = _context.Qestions.Where(p => p.Id == questionId)
            .AsNoTracking()
            .FirstOrDefault();
            var result = new GetQuestionDitelWithAnswersPager() { };
            if (questione != null)
            {
                var model = _mapper.Map<GetQuestionViewModel>(questione);


                result.GetQuestionDto = model;
                return new ResultDto<GetQuestionDitelWithAnswersPager>
                {
                    Data = result,
                    IsSuccess = true,
                    Message = ""
                };

            }

            return new ResultDto<GetQuestionDitelWithAnswersPager>
            {
                Data = result,
                IsSuccess = false,
                Message = ""
            };
        }
    }
}
