using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Question;
using Azmoon.Application.Service.Question.Dto;
using Azmoon.Common.ResultDto;
using Azmoon.Common.Useful;

namespace Azmoon.Application.Service.Question.Command
{
    public class AddQuestion : IAddQuestion
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly Common.FileWork.IFileProvider _fileProvider;

        public AddQuestion(IDataBaseContext context, IMapper mapper, Common.FileWork.IFileProvider fileProvider)
        {
            _context = context;
            _mapper = mapper;
            _fileProvider = fileProvider;
        }

        public ResultDto<AddQuestionViewModel> Execute(AddQuestionViewModel dto)
        {

            var question = _mapper.Map<Domain.Entities.Question>(dto);

            if (question.Id > 0)
            {
                _context.Qestions.Update(question);
            }
            else
            {
                _context.Qestions.Add(question);
            }

            var result = _context.SaveChanges();
            if (result>0)
            {
                return new ResultDto<AddQuestionViewModel>
                {
                    Data = dto,
                    IsSuccess = true,
                    Message = "موفق"
                };
            }
            return new ResultDto<AddQuestionViewModel>
            {
                Data = null,
                IsSuccess = false,
                Message = "نا موفق"
            };
        }


    }
}
