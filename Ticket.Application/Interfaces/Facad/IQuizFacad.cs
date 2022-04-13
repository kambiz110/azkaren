using Azmoon.Application.Interfaces.Quiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Interfaces.Facad
{
   public interface IQuizFacad
    {
        IAddQuiz addQuiz { get; }
        IGetQuiz getQuiz { get; }
        IGetQuizForStudendt getQuizForStudendt { get; }
    }
}
