using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Service.User.Dto;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Interfaces.User
{
   public interface ICreateUser
    {
        ResultDto<Domain.Entities.User> Register(RegisterUserDto dto, IFormFile Image);
    }
}
