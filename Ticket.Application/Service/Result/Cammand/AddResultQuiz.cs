using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Result;
using Azmoon.Application.Service.Result.Dto;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Result.Cammand
{
    public class AddResultQuiz : IAddResultQuiz
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public AddResultQuiz(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ResultDto<AddResultQuizDto> addResultQuiz(AnswerQuizStudendt dto)
        {
            var quiz = _context.Quizzes
                        .Where(p => p.Id == dto.QuizId)
                        .AsNoTracking()
                        .FirstOrDefault();
            int TrueAnswer = 0;
            int FalseAnswer = 0;
            string answeresInQuiz = "";
            foreach (var item in dto.questions)
            {
                var answer = _context.Answers
                        .Where(p => p.Id == item.AnswereId)
                        .AsNoTracking()
                        .FirstOrDefault();

                if (answer.IsTrue)
                {
                    TrueAnswer++;
                }
                else
                {
                    FalseAnswer++;
                }
                answeresInQuiz = answeresInQuiz + item.QuestionId + ":" + item.AnswereId + "|";

            }

            //////////////////////////////////////////////////////
            AddResultQuizDto resultDto = new AddResultQuizDto();

            resultDto.Points = TrueAnswer;
            resultDto.MaxPoints = quiz.MaxQuestion;
            resultDto.QuizId = quiz.Id;
            resultDto.StartQuiz = dto.StartQuiz;
            resultDto.EndQuiz = dto.EndQuiz;
            resultDto.StudentId= _context.Users
                        .Where(p => p.UserName == dto.StudentId)
                        .AsNoTracking()
                        .FirstOrDefault().Id;
            resultDto.AnsweresInQuiz = answeresInQuiz;
            var model = _mapper.Map<Domain.Entities.Result>(resultDto);
            model.UpdatedAt = DateTime.Now;
            if (model.Id > 0)
            {
                _context.Results.Update(model);
            }
            else
            {
                _context.Results.Add(model);
            }

            var result = _context.SaveChanges();
            if (result > 0)
            {
                resultDto.Id = model.Id;
                return new ResultDto<AddResultQuizDto>
                {
                    Data = resultDto,
                    IsSuccess = true,
                    Message = "Success"
                };
            }
            return new ResultDto<AddResultQuizDto>
            {
                Data = resultDto,
                IsSuccess = false,
                Message = "Warninge"
            };
        }
    }
}
