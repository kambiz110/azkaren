using Azmoon.Application.Interfaces.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Interfaces.Facad
{
  public interface IResultQuizFacad
    {
        IAddResultQuiz addResultQuiz { get; }
        IGetResultQuiz getResultQuiz { get; }
    }
}
