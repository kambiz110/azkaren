using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.User;
using Azmoon.Application.Service.User.Dto;
using Azmoon.Common.ResultDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.User.Command
{
    public class RegisterUser : IRegisterUser
    {
        private readonly IDataBaseContext _context;
        private readonly UserManager<Domain.Entities.User> _userManger;
        private readonly RoleManager<Domain.Entities.Role> _roleManger;

        public RegisterUser(IDataBaseContext context, UserManager<Domain.Entities.User> userManger, RoleManager<Domain.Entities.Role> roleManger)
        {
            _context = context;
            _userManger = userManger;
            _roleManger = roleManger;
        }

        public ResultDto<int> registerExqute(ShortRegisterDto dto)
        {
            var ExistUser = _context.Persons.Where(p => p.personeli == dto.personeli.ToString()).AsNoTracking().FirstOrDefault();
            if (ExistUser!=null)
            {
                var userFind = _userManger.FindByNameAsync(dto.personeli.ToString()).Result;
                if (userFind != null)
                {
                    var user = new Domain.Entities.User
                    {
                        FirstName = ExistUser.name,
                        LastName = ExistUser.family,
                        Phone = dto.Phone.ToString(),
                        UserName = ExistUser.personeli.ToString(),
                        Email = dto.personeli + "@Saas.ir",
                        NormalizedEmail = dto.personeli + "@Saas.ir",
                        NormalizedUserName = dto.personeli.ToString(),
                        LockoutEnabled = false,
                        SecurityStamp = Guid.NewGuid().ToString(),

                    };

                    var result = _userManger.CreateAsync(user, dto.Password).Result;
                    if (result.Succeeded)
                    {
                        _userManger.AddToRoleAsync(user, "User");
                        return new ResultDto<int>
                        {

                            IsSuccess = true,
                            Message = "موفق"
                        };
                    }
                }

                return new ResultDto<int>
                {
                    Data = 1,
                    IsSuccess =false,
                Message= "کاربر موجود بود"
                };
            }
            else
            {
              

                 return new ResultDto<int>
                 {
                Data=0,
                IsSuccess = false,
                Message = "کاربری در دیتابیس اولیه نبود"
            };
            
             
            }
         
        }
    }
}
