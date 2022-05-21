using AutoMapper;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Interfaces.Result;
using Azmoon.Application.Service.Result.Cammand;
using Azmoon.Application.Service.Result.Query;
using Azmoon.Application.Service.WorkPlace.Query;
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
        private readonly IGetWorkplacFirstToEndParent _workplacFirstToEndParent;
        public ResultQuizFacad(IDataBaseContext context, IMapper mapper, IGetWorkplacFirstToEndParent workplacFirstToEndParent)
        {
            _context = context;
            _mapper = mapper;
            _workplacFirstToEndParent = workplacFirstToEndParent;
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
                return _getResultQuiz = _getResultQuiz ?? new GetResultQuiz(_context, _mapper , _workplacFirstToEndParent);
            }
        }
        private IAutorizResultInDb _resultInDb;
        public IAutorizResultInDb autorizResultIn
        {
            get
            {
                return _resultInDb = _resultInDb ?? new AutorizResultInDb(_context);
            }
        }
    }
}
