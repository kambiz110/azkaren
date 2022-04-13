using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Answer;

namespace Azmoon.Application.Interfaces.Facad
{
  public  interface IAnswerFacad
    {
        IAddAnswer AddAnswer { get; }
        IGetAnswer GetAnswer { get; }
    }
}
