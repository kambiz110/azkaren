using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Filter;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Filter.Command
{
    public class DeleteQuizFilter : IDeleteQuizFilter
    {
        private readonly IDataBaseContext _context;

        public DeleteQuizFilter(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task< ResultDto> deleteFilterAsync(long quizId, long filterId)
        {
            var quizFilter = _context.QuizFilters.Where(p => p.QuizId == quizId && p.Id == filterId).FirstOrDefault();
            if (quizFilter!=null)
            {
                _context.QuizFilters.Remove(quizFilter);
                await _context.SaveChangesAsync();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "موفق"
                };
            }
            return new ResultDto { 
            IsSuccess=false,
            Message="ناموفق"
            };
        }
    }
}
