using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Interfaces.User
{
    public interface IDeleteRoleInUser
    {
        ResultDto Exequte(string UserId, string RoleId, string userName);
    }
}
