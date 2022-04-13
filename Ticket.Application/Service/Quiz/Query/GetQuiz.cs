using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Quiz;
using Azmoon.Application.Service.Quiz.Dto;
using Azmoon.Common.Pagination;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Service.Question.Dto;
using Azmoon.Common.Useful;

namespace Azmoon.Application.Service.Quiz.Query
{
    public class GetQuiz : IGetQuiz
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public GetQuiz(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ResultDto<AddQuizDto> GetQuizById(long id)
        {
            var model = _context.Quizzes.Where(p => p.Status == 1 && p.Id == id)
            .AsNoTracking()
            .FirstOrDefault();
            if (model != null)
            {
                var result = _mapper.Map<AddQuizDto>(model);

                return new ResultDto<AddQuizDto>
                {

                    Data = result,
                    IsSuccess = true,
                    Message = "Success"
                };
            }
            return new ResultDto<AddQuizDto>
            {
                Data = null,
                IsSuccess = false,
                Message = "Warninge"
            };
        }
        public ResultDto<GetQuizDetilesDto> GetQuizDetailsById(long id)
        {
            var model = _context.Quizzes.Where(p => p.Status == 1 && p.Id == id)
            .AsNoTracking()
            .Include(p=>p.Group)
            .FirstOrDefault();
            if (model != null)
            {
                var result = _mapper.Map<GetQuizDetilesDto>(model);

                return new ResultDto<GetQuizDetilesDto>
                {

                    Data = result,
                    IsSuccess = true,
                    Message = "Success"
                };
            }
            return new ResultDto<GetQuizDetilesDto>
            {
                Data = null,
                IsSuccess = false,
                Message = "Warninge"
            };
        }

        public ResultDto<GetQuizDetWithQuestionPager> GetQuizDetWithQuestionPagerById(long id)
        {
            var result = new GetQuizDetWithQuestionPager() { };
            var model = _context.Quizzes.Where(p => p.Status == 1 && p.Id == id)
            .AsNoTracking()
            .Include(p => p.Group)
            .FirstOrDefault();
            if (model != null)
            {
                var mapModel = _mapper.Map<GetQuizDetilesDto>(model);
                result.GetQuizDetiles = mapModel;
                return new ResultDto<GetQuizDetWithQuestionPager>
                {

                    Data = result,
                    IsSuccess = true,
                    Message = "Success"
                };
            }
            return new ResultDto<GetQuizDetWithQuestionPager>
            {
                Data = result,
                IsSuccess = false,
                Message = "Warninge"
            };
        }

        public ResultDto<GetQuizWithPeger> GetQuizes(int PageSize, int PageNo, string searchKey, int status)
        {
            int rowCount = 0;
  
            var model = _context.Quizzes.Where(p => p.Status == status)
                .AsNoTracking()
                .OrderByDescending(p=>p.StartDate)
                .AsQueryable();
            if (!String.IsNullOrEmpty(searchKey))
            {
                model = model.Where(p => p.Name.Contains(searchKey.Trim())).AsQueryable();
            }
            var date = new GetQuizWithPeger() { };
            if (model != null)
            {
               var modelList= model.ToPaged(PageNo, PageSize, out rowCount).ToList();
                var result = _mapper.Map<List<GetQuizDto>>(modelList);
                var paging = result.ToPaged(PageNo, PageSize, out rowCount).ToList();
                for (int i = 0; i < paging.Count; i++)
                {
                    var start = (DateTime)modelList.ElementAt(i).StartDate;
                    var startSuspand = start.AddHours(5.0);
                    var end = (DateTime)modelList.ElementAt(i).EndDate;
                    if (start <= DateTime.Now && end>DateTime.Now)
                    {
                        //در حال برگزاری
                        paging.ElementAt(i).state = 1;
                    }
                    if (startSuspand >= DateTime.Now && start > DateTime.Now)
                    {
                        //در انتظار شروع
                        paging.ElementAt(i).state = 2;
                    }
                    if (end < DateTime.Now)
                    {
                        //خاتمه یافته
                        paging.ElementAt(i).state = 3;
                    }
                }
                date.Quizes = paging;
                date.PagerDto = new PagerDto
                {
                    PageNo = PageNo,
                    PageSize = PageSize,
                    TotalRecords = rowCount
                };
                return new ResultDto<GetQuizWithPeger>
                {

                    Data = date,
                    IsSuccess = true,
                    Message = "Success"
                };
            }
            return new ResultDto<GetQuizWithPeger>
            {
                Data = null,
                IsSuccess = false,
                Message = "Warninge"
            };
        }

        public ResultDto<long> GetQuizIdByPasswordAsync(string password)
        {
            var model = _context.Passwords.Where(p => p.Status == 1  && p.Content==password.ToEncodeAndHashMD5())
           .AsNoTracking()
           .FirstOrDefault();
            if (model != null)
            {
                return new ResultDto<long>
                {
                    Data=model.Id,
                    IsSuccess = true,
                    Message = "Success"
                };
            }
            return new ResultDto<long>
            {

                IsSuccess = false,
                Message = "Warninge"
            };
        }

        public async Task<AttemtedQuizViewModel> GetQuizByIdAsync(long id)
        {
            var getQuizForStudendtDto = new AttemtedQuizViewModel();
            var Quiz = _context.Quizzes.Where(p => p.Id == id)
                .AsNoTracking()
                .FirstOrDefault();
            var Question = _context.Qestions
                .Where(p => p.QuizId == id)
                .AsNoTracking()
                .ToList();
            List<AttemtedQuizQuestionViewModel> qustiones = new List<AttemtedQuizQuestionViewModel>();
            int QuestionCounter = 0;
            if (Question.Count > Quiz.MaxQuestion)
            {
                QuestionCounter = Quiz.MaxQuestion;
            }
            else
            {
                QuestionCounter = Question.Count();
            }
            var lstIndexQuestion = getArreyIndex(qustiones.Count(), QuestionCounter);
            foreach (var item in lstIndexQuestion)
            {
                var ques = new AttemtedQuizQuestionViewModel
                {
                    Id = Question[item].Id,
                    Text = Question[item].Text,
                    Answers = await GetAnswerForQuestionId(Question[item].Id)
                };
                qustiones.Add(ques);
            }
            getQuizForStudendtDto.Id = id;
            getQuizForStudendtDto.Name = Quiz.Name;
            getQuizForStudendtDto.Questions = qustiones;
            getQuizForStudendtDto.Timer = (int)Quiz.Timer;
            return getQuizForStudendtDto;
        }

        private IList<int> getArreyIndex(int length, int count)
        {

            Random rndQusetionIndex = new Random();

            IList<int> result = new List<int>();
            do
            {
                var a = rndQusetionIndex.Next(0, (length - 1));
                if (!result.Where(p => p == a).Any())
                {
                    result.Add(a);
                }

            }
            while (result.Count() <= count);

            return result;
        }

        private async Task< List<AttemtedQuizAnswerViewModel>> GetAnswerForQuestionId(long questionId)
        {
            List<AttemtedQuizAnswerViewModel> result = new List<AttemtedQuizAnswerViewModel>();
            var answers = await _context.Answers
             .Where(p => p.QuestionId == questionId)
             .AsNoTracking()
             .ToListAsync();
            var lstIndexAnswers = getArreyIndex(answers.Count(), 4);
            foreach (var item in lstIndexAnswers)
            {
                var answ = new AttemtedQuizAnswerViewModel
                {
                    Id = answers[item].Id,
                    Text = answers[item].Text,
                    IsTrue = answers[item].IsTrue,
                    QuestionId= questionId
                };
                result.Add(answ);
            }
            return result;
        }
    }
}
