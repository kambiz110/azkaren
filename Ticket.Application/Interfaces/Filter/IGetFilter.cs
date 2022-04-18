using Azmoon.Application.Service.Filter.Dto;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Interfaces.Filter
{
   public interface IGetFilter
    {
        ResultDto<CreatFilterDto> GetByQuizId(long id);
    }
}
