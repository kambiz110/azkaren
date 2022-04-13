using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Answer;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Facad;

using Azmoon.Common.FileWork;
using Azmoon.Application.Service.Answer.Command;
using Azmoon.Application.Service.Answer.Query;

namespace Azmoon.Application.Service.Facad
{
    public class AnswerFacad : IAnswerFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public AnswerFacad(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private IAddAnswer _addAnswer;
        public IAddAnswer AddAnswer
        {
            get 
            {
                return _addAnswer = _addAnswer ?? new AddAnswer(_context ,_mapper);
            }
        }
        private IGetAnswer _getAnswer;
        public IGetAnswer GetAnswer
        {
            get 
            { 
            return _getAnswer= _getAnswer?? new GetAnswer(_context, _mapper);
            }
        }
    }
}
