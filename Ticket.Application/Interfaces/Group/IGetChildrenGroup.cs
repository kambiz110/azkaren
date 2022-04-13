using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Interfaces.Group
{
   public interface IGetChildrenGroup
    {
        ResultDto<List<long>> Exequte(List<long> workplaceIdes);
        ResultDto<List<long>> ExequteById(long? workplaceId);
    }
}
