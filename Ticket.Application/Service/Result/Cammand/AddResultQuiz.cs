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
using Azmoon.Common.Useful;

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
        public ResultDto<AddResultQuizDto> addResultQuiz(DataResultQuizDto dto)
        {
            var quiz = _context.Quizzes
                        .Where(p => p.Id == dto.QuizId)
                        .AsNoTracking()
                        .FirstOrDefault();
            var quizStartTemps = _context.QuizStartTemps.Where(p => p.QuizId == dto.QuizId && p.UserName == dto.username && p.RegesterAt > quiz.StartDate).FirstOrDefault();
            if (dto.answer != null && dto.answer.Count() > 0)
            {
                var questiones = dto.answer.Select(p => new { key = Int64.Parse(p.Key) }).ToArray();
                var answeres = dto.answer.Select(p => new { value = Int64.Parse(p.Value) }).ToArray();

                int TrueAnswer = 0;
                int FalseAnswer = 0;
                string answeresInQuiz = "";



                for (int i = 0; i < questiones.Length; i++)
                {

                    var answer = _context.Answers
                                                .Where(p => p.Id == answeres[i].value && p.QuestionId == questiones[i].key)
                                                .AsNoTracking()
                                                .FirstOrDefault();
                    if (answer != null)
                    {
                        if (answer.IsTrue)
                        {
                            TrueAnswer++;
                        }
                        else
                        {
                            FalseAnswer++;
                        }
                        answeresInQuiz = answeresInQuiz + questiones[i].key + ":" + answeres[i].value + "|";
                    }
                }
                AddResultQuizDto resultDto = new AddResultQuizDto();
                resultDto.Ip = dto.Ip;
                resultDto.Points = TrueAnswer;
                resultDto.MaxPoints = quiz.MaxQuestion;
                resultDto.QuizId = quiz.Id;
                resultDto.StartQuiz = quizStartTemps.StartDate;
                resultDto.EndQuiz = DateTime.Now;
                resultDto.AuthorizationResult= answeresInQuiz.EncryptStringWithKey(dto.username+dto.QuizId.ToString());
                resultDto.StudentId = _context.Users
                            .Where(p => p.UserName == dto.username)
                            .AsNoTracking()
                            .FirstOrDefault().Id;
                resultDto.AnsweresInQuiz = answeresInQuiz;


                //var test = resultDto.AuthorizationResult.DecryptStringWithKey(dto.username + dto.QuizId.ToString());
                var model = _mapper.Map<Domain.Entities.Result>(resultDto);

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
            }


            //////////////////////////////////////////////////////
           
      
            return new ResultDto<AddResultQuizDto>
            {
                Data = null,
                IsSuccess = false,
                Message = "Warninge"
            };
        }
    }
}
