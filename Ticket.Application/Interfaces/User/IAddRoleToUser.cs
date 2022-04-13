using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Interfaces.User
{
   public interface IAddRoleToUser
    {
        ResultDto Exequte(string roleId, string userId, string userName);
    }
}
