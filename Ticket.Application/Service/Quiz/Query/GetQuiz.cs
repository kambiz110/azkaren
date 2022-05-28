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
using Azmoon.Application.Service.Group.Query;
using Microsoft.AspNetCore.Mvc.Rendering;
using Azmoon.Application.Interfaces.Group;

namespace Azmoon.Application.Service.Quiz.Query
{
    public class GetQuiz : IGetQuiz
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IGetChildrenGroup _getChildrenGroup;
        public GetQuiz(IDataBaseContext context, IMapper mapper, IGetChildrenGroup getChildrenGroup)
        {
            _context = context;
            _mapper = mapper;
            _getChildrenGroup = getChildrenGroup;
        }

        public ResultDto<AddQuizDto> GetQuizById(long id)
        {

            var model = _context.Quizzes.Where(p => p.Status == 1 && p.Id == id)
            .AsNoTracking()
            .FirstOrDefault();
            if (model != null)
            {
                var result = _mapper.Map<AddQuizDto>(model);
                if (!String.IsNullOrEmpty(result.Password))
                {
                    result.Password = result.Password.DecryptString();
                }

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
            .Include(p => p.Group)
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

        public ResultDto<GetQuizWithPeger> GetQuizes(int PageSize, int PageNo, string searchKey, int status , string username)
        {
            int rowCount = 0;
            var user = _context.Users.Where(p => p.UserName == username)
                .Include(p => p.GroupUsers)
                .FirstOrDefault();
            var groupIdes = _getChildrenGroup.Exequte(user.GroupUsers.Select(p=>p.GroupId).ToList()).Data;
            var model = _context.Quizzes.Where(p => p.Status == status )
                .AsNoTracking()
                .OrderByDescending(p => p.StartDate)
                .Include(p => p.QuizFilter)
                .Include(p=>p.Group)
                .AsQueryable();
            if (user.GroupUsers!=null && user.GroupUsers.Count()>0)
            {
                model = model.Where(p => groupIdes.Contains(p.GroupId) || p.CreatorId == user.Id).AsQueryable();
            }
            else
            {
                model = model.Where(p => p.CreatorId == user.Id).AsQueryable();
            }
            

            if (!String.IsNullOrEmpty(searchKey))
            {
                model = model.Where(p => p.Name.Contains(searchKey.Trim())).AsQueryable();
            }
            var date = new GetQuizWithPeger() { };
            if (model != null)
            {
                var modelList = model.ToPaged(PageNo, PageSize, out rowCount).ToList();
                var result = _mapper.Map<List<GetQuizDto>>(modelList);
                var paging = result.ToPaged(PageNo, PageSize, out rowCount).ToList();
                for (int i = 0; i < paging.Count; i++)
                {
                    var start = (DateTime)modelList.ElementAt(i).StartDate;
                    var startSuspand = start.AddHours(5.0);
                    var end = (DateTime)modelList.ElementAt(i).EndDate;
                    if (start <= DateTime.Now && end > DateTime.Now)
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

        public ResultDto<long> GetQuizIdByPasswordAsync(string password, long quizId , string userId)
        {

            var model = _context.Quizzes.Where(p => p.Status == 1 && p.Id == quizId &&p.StartDate<DateTime.Now && p.EndDate> DateTime.Now)
                 .Include(p => p.Results.Where(p=>p.StudentId==userId))
           .AsNoTracking()
           .AsQueryable();

            if (!String.IsNullOrEmpty(password))
            {
                model= model.Where(p=>p.Password == password.EncryptString()).AsQueryable();
            }
            else
            {
                var passmodel = model.Where(p=>p.Password!=null).FirstOrDefault();
                if (passmodel != null && passmodel.Results.Count == 0)
                {
                    return new ResultDto<long>
                    {
                        Data = 0,
                        IsSuccess = false,
                        Message = "شما باید در قسمت آزمون های دارای رمز شرکت نمایید!!!"
                    };
                }
            }
            var Rmodel = model.FirstOrDefault();
            if (Rmodel != null &&  Rmodel.Results.Count==0)
            {
                return new ResultDto<long>
                {
                    Data= Rmodel.Id,
                    IsSuccess = true,
                    Message = "Success"
                };
            }
            return new ResultDto<long>
            {

                IsSuccess = false,
                Message = "شما یا در این آزمون شرکت نموده اید و یا اطلاعات و دسترسی لازم برای  این آزمون را ندارید!!"
            };
        }
        public ResultDto<GetQuizDetilesViewModel> GetQuizViewStartPageById(long id) {

            var model = _context.Quizzes.Where(p => p.Status == 1 && p.Id == id)
               .AsNoTracking()
               .Include(p => p.Questions)
               .FirstOrDefault();
            if (model != null)
            {
                return new ResultDto<GetQuizDetilesViewModel>
                {
                    Data = new GetQuizDetilesViewModel
                    {
                        Description = model.Description,
                        Id = model.Id,
                        Name = model.Name,
                        Timer = (int)model.Timer,
                        QuestionCount = model.MaxQuestion
                    },
                    IsSuccess=true
                };
            }
             return new ResultDto<GetQuizDetilesViewModel>
            {
                Data=null,
                IsSuccess=false
            }; 

}
        public async Task<AttemtedQuizViewModel> GetQuizByIdAsync(long id)
        {
            var getQuizForStudendtDto = new AttemtedQuizViewModel();
            var Quiz = _context.Quizzes.Where(p => p.Id == id)
                .AsNoTracking()
                .FirstOrDefault();
            var Question = _context.Qestions
                .Where(p => p.QuizId == id && p.Status==1)
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

            var lstIndexQuestion = getArreyIndex(Question.Count, QuestionCounter);

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
            getQuizForStudendtDto.Description = Quiz.Description;
            getQuizForStudendtDto.Name = Quiz.Name;
            getQuizForStudendtDto.Questions = qustiones;
            getQuizForStudendtDto.Timer = (int)Quiz.Timer;
            return getQuizForStudendtDto;
        }

        private IList<int> getArreyIndex(int length, int count)
        {
            IList<int> result = new List<int>();
            Random rndQusetionIndex = new Random();
            if (length<count)
            {
                count = length;
            }
            if (length == count && count == 1)
            {
                result.Add(0);
            }
            else
            {
                do
                {
                    var a = rndQusetionIndex.Next(0, (length));
                    if (!result.Where(p => p == a).Any())
                    {
                        result.Add(a);
                    }

                }
                while (result.Count() < count);
                return result;
            }



            return result;
        }
        private IList<long> getArreyIndexAnswer(int length, int count, List<long> Ides)
        {

            Random rndQusetionIndex = new Random();

            IList<long> result = new List<long>();
            int TrueCounter = 0;
            int FalseCounter = 0;
            do
            {
                var a = rndQusetionIndex.Next(0, (length));


                if (!result.Where(p => p == Ides[a]).Any())
                {
                    var answers = _context.Answers
                         .Where(p => p.Id == Ides[a])
                         .AsNoTracking()
                         .FirstOrDefault();
                    if (answers.IsTrue)
                    {
                        if (TrueCounter < 1)
                        {

                            result.Add(Ides[a]);
                            TrueCounter++;



                        }

                    }
                    else
                    {
                        if (FalseCounter < 3)
                        {

                            result.Add(Ides[a]);
                            FalseCounter++;


                        }

                    }



                }

            }
            while (result.Count() < count);

            return result;
        }
        private async Task<List<AttemtedQuizAnswerViewModel>> GetAnswerForQuestionId(long questionId)
        {
            List<AttemtedQuizAnswerViewModel> result = new List<AttemtedQuizAnswerViewModel>();
            var answers = await _context.Answers
             .Where(p => p.QuestionId == questionId && p.Status==1)
             .AsNoTracking()
             .ToListAsync();

            var Ides = answers.Select(p => p.Id).ToList();
            var lstIndexAnswers = getArreyIndexAnswer(answers.Count(), 4, Ides);
            foreach (var item in lstIndexAnswers)
            {
           
                var answ = new AttemtedQuizAnswerViewModel
                {
                    Id = answers.Find(p => p.Id == item).Id,
                    Text = answers.Find(p => p.Id == item).Text,
                    //IsTrue = answers[item].IsTrue,
                    QuestionId = questionId
                };
                result.Add(answ);
            }
            return result;
        }
    }
}
