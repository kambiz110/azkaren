using Azmoon.Application.Interfaces.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Interfaces.Facad
{
   public interface IQuizFilterFacad
    {
        IAddQuizFilter addQuizFilter { get; }
        IGetFilter getFilter { get; }
        IDeleteQuizFilter deleteQuizFilter { get; }
    }
}
