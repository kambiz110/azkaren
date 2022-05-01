using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Interfaces.Group
{
   public interface IDeleteGroupAccess
    {
        ResultDto delete(string UserId, long groupId);
    }
}
