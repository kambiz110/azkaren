using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Service.Answer.Dto;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Interfaces.Answer
{
   public interface IAddAnswer
    {
        ResultDto<AddAnswerDto> Exequte(AddAnswerDto dto, string userName);
    }
}
