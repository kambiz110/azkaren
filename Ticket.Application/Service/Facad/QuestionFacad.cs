using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Interfaces.Question;
using Azmoon.Application.Service.Question.Command;
using Azmoon.Application.Service.Question.Query;
using Azmoon.Common.FileWork;

namespace Azmoon.Application.Service.Facad
{
    public class QuestionFacad : IQuestionFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly Common.FileWork.IFileProvider _fileProvider;


        private IAddQuestion _addQuestion;

        public QuestionFacad(IDataBaseContext context, IMapper mapper, IFileProvider fileProvider)
        {
            _context = context;
            _mapper = mapper;
            _fileProvider = fileProvider;
        }

        public IAddQuestion AddQuestion 
        {
            get
            {
                return _addQuestion = _addQuestion ?? new AddQuestion(_context ,_mapper , _fileProvider);
            }
        }
        private IGetQuestion _qetQuestion;
        public IGetQuestion GetQuestion
        {
            get
            {
                return _qetQuestion = _qetQuestion ?? new GetQuestion(_context, _mapper);
            }
        }
   
    }
}
