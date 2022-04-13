using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Interfaces.Tag
{
   public interface IAddTag
    {
        ResultDto Add(string tag, long questionId);
        ResultDto AddQuestionTag(long tagId , long questionId);
        ResultDto RemoveQuestionTag(string tag, long questionId);
    }
}
