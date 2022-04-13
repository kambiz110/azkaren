using AutoMapper;
using Azmoon.Application.Interfaces.Answer;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Service.Answer.Dto;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Answer.Command
{
    public class AddAnswer : IAddAnswer
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public AddAnswer(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ResultDto<AddAnswerDto> Exequte(AddAnswerDto dto , string userName)
        {
            var user = _context.Users.Where(p => p.UserName == userName).FirstOrDefault();
            var answer = _mapper.Map<Domain.Entities.Answer>(dto);
            answer.UserId = user.Id;

            if (answer.Id > 0)
            {
                _context.Answers.Update(answer);
            }
            else
            {
                _context.Answers.Add(answer);
            }

            var result = _context.SaveChanges();
            if (result > 0)
            {
                return new ResultDto<AddAnswerDto>
                {
                    Data = dto,
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
    }
}
