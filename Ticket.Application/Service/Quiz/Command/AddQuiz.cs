using AutoMapper;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Quiz;
using Azmoon.Application.Service.Quiz.Dto;
using Azmoon.Common.ResultDto;
using Azmoon.Common.Useful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Quiz
{
    public class AddQuiz : IAddQuiz
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public AddQuiz(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ResultDto<AddQuizDto> Exequte(AddQuizDto dto, string userName)
        {
            var user = _context.Users.Where(p => p.UserName == userName).FirstOrDefault();
            var model = _mapper.Map<Domain.Entities.Quiz>(dto);
            model.Password = model.Password!=null? dto.Password.EncryptString():null;
            model. CreatorId = user.Id;
            model.UpdatedAt = DateTime.Now;
            if (model.Id > 0)
            {
                //_context.Passwords.Add(new Domain.Entities.Password { QuizId=model.Id , Content=model.Password.ToEncodeAndHashMD5() });
            _context.Quizzes.Update(model);
            }
            else
            {
                _context.Quizzes.Add(model);
                //var pass = _context.Passwords.Where(p => p.QuizId == model.Id).FirstOrDefault();
                //pass.Content = model.Password;
                //_context.Passwords.Update(pass);
            }

            var result = _context.SaveChanges();
            if (result > 0)
            {
                dto.Id = model.Id;
                return new ResultDto<AddQuizDto>
                {
                    Data = dto,
                    IsSuccess = true,
                    Message = "Success"
                };
            }
            return new ResultDto<AddQuizDto>
            {
                Data = dto,
                IsSuccess = false,
                Message = "Warninge"
            };
        }
    }
}
