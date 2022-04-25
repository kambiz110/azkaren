using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Quiz;
using Azmoon.Application.Service.Quiz.Dto;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Common.Pagination;
using Azmoon.Application.Service.Filter.Dto;
using Azmoon.Common.Useful;

namespace Azmoon.Application.Service.Quiz.Query
{
    public class GetQuizForStudendt : IGetQuizForStudendt
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public GetQuizForStudendt(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ResultDto<GetQuizForStudendtDto> Exequte(long QuizId)
        {
            var getQuizForStudendtDto = new GetQuizForStudendtDto();
            var Quiz = _context.Quizzes.Where(p => p.Id == QuizId)
                .AsNoTracking()
                .FirstOrDefault();
            var Question = _context.Qestions
                .Where(p => p.QuizId == QuizId)
                .AsNoTracking()
                .ToList();
            List<QustionesInQuiz> qustiones = new List<QustionesInQuiz>();


            int QuestionCounter = 0;
            if (Question.Count> Quiz.MaxQuestion)
            {
                QuestionCounter = Quiz.MaxQuestion;
            }
            else
            {
                QuestionCounter = Question.Count();
            }

            var lstIndexQuestion = getArreyIndex( qustiones.Count(), QuestionCounter);

            foreach (var item in lstIndexQuestion)
            {
                var ques = new QustionesInQuiz {
                    QuestionId = Question[item].Id,
                    QustionText = Question[item].Text,
                    answers = GetAnswerForQuestionId(Question[item].Id)
                };
                qustiones.Add(ques);
            }

            throw new NotImplementedException();
        }
        public  ResultDto<GetQuizStudentWithPeger> GetQuizes(int PageSize, int PageNo, string searchKey, int status)
        {
            int rowCount = 0;

            var model =  _context.Quizzes.Where(p => p.Status == status)
                .AsNoTracking()
                .OrderByDescending(p => p.StartDate)
                .Include(p => p.QuizFilter)
                .AsQueryable();
            if (!String.IsNullOrEmpty(searchKey))
            {
                model = model.Where(p => p.Name.Contains(searchKey.Trim())).AsQueryable();
            }
            if (status==1)
            {
                model = model.Where(p => p.StartDate <= DateTime.Now && p.EndDate > DateTime.Now).AsQueryable();
            }
            if (status == 2)
            {
               model = model.Where(p => p.StartDate > DateTime.Now && p.StartDate <= DateTime.Now.AddDays(7) && p.EndDate > DateTime.Now).AsQueryable();
            }
            if (status == 3)
            {
              model = model.Where(p =>  p.EndDate < DateTime.Now).AsQueryable();
            }
            var date = new GetQuizStudentWithPeger() { };
            if (model != null)
            {
                var modelList = model.ToPaged(PageNo, PageSize, out rowCount).ToList();
                var result = _mapper.Map<List<QuizAssignViewModel>>(modelList);
                var paging = result.ToPaged(PageNo, PageSize, out rowCount).ToList();
                for (int i = 0; i < paging.Count; i++)
                {
                    var start = (DateTime)paging.ElementAt(i).StartDate;
                    var end = (DateTime)paging.ElementAt(i).EndDate;

                }
                date.Quizes = paging;
                date.PagerDto = new PagerDto
                {
                    PageNo = PageNo,
                    PageSize = PageSize,
                    TotalRecords = rowCount
                };
                return new ResultDto<GetQuizStudentWithPeger>
                {

                    Data = date,
                    IsSuccess = true,
                    Message = "Success"
                };
            }
            return new ResultDto<GetQuizStudentWithPeger>
            {
                Data = null,
                IsSuccess = false,
                Message = "Warninge"
            };
        }

        private IList<int> getArreyIndex(int length ,int count) {

            Random rndQusetionIndex = new Random();

            IList<int> result = new List<int>();
            do
            {
                var a = rndQusetionIndex.Next(0, (length - 1));
                if (!result.Where(p=>p ==a).Any())
                {
                    result.Add(a);
                }
               
            }
            while (result.Count() <= count);
           
            return result;
        }

        private List<AnswerInQustion> GetAnswerForQuestionId(long questionId) {
            List<AnswerInQustion> result = new List<AnswerInQustion>();
            var answers = _context.Answers
             .Where(p => p.QuestionId == questionId)
             .AsNoTracking()
             .ToList();
            var lstIndexAnswers = getArreyIndex(answers.Count(), 4);
            foreach (var item in lstIndexAnswers)
            {
                var answ = new AnswerInQustion
                {
                    AnswereId = answers[item].Id,
                    Text = answers[item].Text,
                    IsTrue= answers[item].IsTrue,
                };
                result.Add(answ);
            }
            return result;
        }
    }

}
