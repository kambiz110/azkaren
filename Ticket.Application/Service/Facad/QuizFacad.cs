using AutoMapper;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Interfaces.Quiz;
using Azmoon.Application.Service.Quiz;
using Azmoon.Application.Service.Quiz.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Facad
{
    public class QuizFacad : IQuizFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public QuizFacad(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private IAddQuiz _addQuiz;
        public IAddQuiz addQuiz
        {
            get
            {
                return _addQuiz = _addQuiz ?? new AddQuiz(_context, _mapper);
            }
        }

        private IGetQuiz _getQuiz;
        public IGetQuiz getQuiz
        {
            get
            {
                return _getQuiz = _getQuiz ?? new GetQuiz(_context, _mapper);
            }
        }

        private IGetQuizForStudendt _quizForStudendt;
        public IGetQuizForStudendt getQuizForStudendt
        {
            get
            {

                return _quizForStudendt = _quizForStudendt ?? new GetQuizForStudendt(_context, _mapper);
            }
        }
    }
}
