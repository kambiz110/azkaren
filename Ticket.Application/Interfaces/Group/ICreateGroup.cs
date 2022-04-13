using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Service.Group.Dto;

using Azmoon.Common.ResultDto;
using Azmoon.Domain.Entities;

namespace Azmoon.Application.Interfaces.Group
{
   public interface ICreateGroup
    {
        ResultDto<Domain.Entities.Group> Exequte(CreateGroupDto dto);
    }
}
