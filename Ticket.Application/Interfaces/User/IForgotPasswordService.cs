using Azmoon.Application.Service.User.Dto;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Interfaces.User
{
   public interface IForgotPasswordService
    {
        ResultDto<string> forgotPassword(ForgotPasswordDto dto);
    }
}
