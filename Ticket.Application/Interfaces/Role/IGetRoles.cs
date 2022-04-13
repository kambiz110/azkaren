using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Service.Role.Dto;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Interfaces.Role
{
   public interface IGetRoles
    {
        ResultDto<List<GetRoleDto>> Exequte();
    }
}
