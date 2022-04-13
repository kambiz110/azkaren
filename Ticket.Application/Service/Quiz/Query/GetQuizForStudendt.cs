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

namespace Azmoon.Application.Service.Quiz.Query
{
    public class GetQuizForStudendt : IGetQuizForStudendt
    {
        private readonly IDataBaseContext _context;


        public GetQuizForStudendt(IDataBaseContext context)
        {
            _context = context;
     
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
