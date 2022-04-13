using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Question;

namespace Azmoon.Application.Interfaces.Facad
{
   public interface IQuestionFacad
    {
        IAddQuestion AddQuestion { get; }
        IGetQuestion GetQuestion { get; }

    }
}
