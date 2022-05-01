using Azmoon.Application.Service.Group.Dto;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Interfaces.Group
{
  public  interface IAddGroupInUser
    {
        ResultDto<GetGroupAccessDto> Exequte(GetGroupAccessDto dto);
    }
}
