using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Interfaces.WorkPlace
{
   public interface IGetChildrenWorkPlace
    {
        ResultDto<List<long>> Exequte(List<long> workplaceIdes);
        ResultDto<List<long>> ExequteById(long? workplaceId);
    }
}
