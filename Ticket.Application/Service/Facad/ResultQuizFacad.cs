using AutoMapper;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Interfaces.Result;
using Azmoon.Application.Service.Result.Cammand;
using Azmoon.Application.Service.Result.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Facad
{
    public class ResultQuizFacad : IResultQuizFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public ResultQuizFacad(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        private IAddResultQuiz _addResultQuiz;
        public IAddResultQuiz addResultQuiz
        {
            get
            {
                return _addResultQuiz = _addResultQuiz ?? new AddResultQuiz(_context, _mapper);
            }
        }



        private IGetResultQuiz _getResultQuiz;
        public IGetResultQuiz getResultQuiz
        {
            get
            {
                return _getResultQuiz = _getResultQuiz ?? new GetResultQuiz(_context, _mapper);
            }
        }
    }
}
