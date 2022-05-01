using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Interfaces.Filter;
using Azmoon.Application.Interfaces.WorkPlace;
using Azmoon.Application.Service.Filter.Command;
using Azmoon.Application.Service.Filter.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Facad
{
    public class QuizFilterFacad : IQuizFilterFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IGetChildrenWorkPlace _getChildrenWorkPlace;
        public QuizFilterFacad(IDataBaseContext context, IGetChildrenWorkPlace getChildrenWorkPlace)
        {
            _context = context;
            _getChildrenWorkPlace = getChildrenWorkPlace;
        }
        private IAddQuizFilter _addQuizFilter;
        public IAddQuizFilter addQuizFilter
        {
            get
            {
                return _addQuizFilter = _addQuizFilter ?? new AddQuizFilter(_context);
            }
        }
        private IGetFilter _getFilter;
        public IGetFilter getFilter
        {
            get
            {
                return _getFilter = _getFilter ?? new GetFilter(_context ,_getChildrenWorkPlace);
            }
        }
        private IDeleteQuizFilter _deleteQuizFilter;
        public IDeleteQuizFilter deleteQuizFilter 
        {
            get
            {
                return _deleteQuizFilter = _deleteQuizFilter ?? new DeleteQuizFilter(_context );
    }
}
    }
}
