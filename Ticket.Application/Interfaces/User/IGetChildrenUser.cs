using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Service.User.Dto;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Interfaces.User
{
   public interface IGetChildrenUser
    {
        ResultDto<GetChildrenUserDto> Exequte(string userName , long[] questionId);
    }
}
