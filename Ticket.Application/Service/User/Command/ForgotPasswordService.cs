using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.User;
using Azmoon.Application.Service.User.Dto;
using Azmoon.Common.ResultDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.User.Command
{
    public class ForgotPasswordService : IForgotPasswordService
    {
        private readonly IDataBaseContext _context;

        public ForgotPasswordService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<string> forgotPassword(ForgotPasswordDto dto)
        {
            var ExistUser = _context.Users.Where(p =>
               p.UserName == dto.personeli.ToString() 
            && p.melli==dto.melli
            &&p.name_father==dto.name_father
            && p.tavalod==dto.tavalod
            && p.name_father==dto.name_father)
                .AsNoTracking()
                .FirstOrDefault();
            if (ExistUser!=null)
            {
                return new ResultDto<string>
                {
                    IsSuccess = true,
                    Data=ExistUser.Id
                };
            }
            return new ResultDto<string>
            {
                IsSuccess = false,
                Message = "کاربر با مشخصات وارد شده یافت نگردید."
            };

        }
    }
}
