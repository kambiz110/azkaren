using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Interfaces.Tag
{
  public interface IDeleteTag
    {
        ResultDto delet(long QuestionId, long TageId);
    }
}
