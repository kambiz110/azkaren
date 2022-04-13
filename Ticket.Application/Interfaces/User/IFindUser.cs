using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Service.User.Dto;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Interfaces.User
{
  public  interface IFindUser
    {
        ResultDto<Domain.Entities.User> Exequte(string id);
        ResultDto<GetDitalesUserProfileDto> GetPerson(string username);
    }
}
