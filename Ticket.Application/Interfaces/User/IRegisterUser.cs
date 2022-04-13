using Azmoon.Application.Service.User.Dto;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Interfaces.User
{
  public interface IRegisterUser
    {
        ResultDto<int> registerExqute(ShortRegisterDto dto);
    }
}
