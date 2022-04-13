using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.User;
using Azmoon.Application.Service.User.Dto;
using Azmoon.Common.ResultDto;
using AutoMapper;
using Azmoon.Domain.Entities;

namespace Azmoon.Application.Service.User.Command
{
    public class CreateUser : ICreateUser
    {
        private readonly IDataBaseContext _context;
        private readonly Common.FileWork.IFileProvider _fileProvider;
        private readonly UserManager<Domain.Entities.User> _userManger;
        private readonly RoleManager<Domain.Entities.Role> _roleManger;
        private readonly IMapper _mapper;
        public CreateUser(IDataBaseContext context, Common.FileWork.IFileProvider fileProvider, UserManager<Domain.Entities.User> userManger, RoleManager<Domain.Entities.Role> roleManger, IMapper mapper)
        {
            _context = context;
            _fileProvider = fileProvider;
            _userManger = userManger;
            _roleManger = roleManger;
            _mapper = mapper;
        }

        public ResultDto<Domain.Entities.User> Register(RegisterUserDto dto, IFormFile Image)
        {


            if (dto.Id!=null && dto.Id != "")
            {
                var userexist = _context.Users.Where(p => p.Id == dto.Id).FirstOrDefault();
                userexist.TypeDarajeh = dto.TypeDarajeh;
                userexist.FirstName = dto.FirstName;
                userexist.LastName = dto.LastName;
                userexist.Phone = dto.Phone.ToString();
                userexist.UserName = dto.personeli.ToString();
                userexist.Email = dto.personeli + "@Saas.ir";
                userexist.NormalizedEmail = dto.personeli + "@Saas.ir";
                userexist.NormalizedUserName = dto.personeli.ToString();
                userexist.LockoutEnabled = false;
                userexist.SecurityStamp = Guid.NewGuid().ToString();
                _context.SaveChanges();
                return new ResultDto<Domain.Entities.User>
                {
                    Data = userexist,
                    IsSuccess = true,
                    Message = "موفق"
                };
            }
            else
            {
                var ExistUser = _context.Users.Where(p => p.UserName == dto.personeli.ToString()).AsNoTracking().FirstOrDefault();
                var RegisteredRole = _context.Roles.Where(p => p.Name == "User").FirstOrDefault();
                if (ExistUser != null)
                {
                    return new ResultDto<Domain.Entities.User>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "کاربر با شماره پرسنلی وارد شده موجود می باشد!!!"
                    };
                }
                var user = new Domain.Entities.User
                {
                    TypeDarajeh=dto.TypeDarajeh,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Phone = dto.melli.ToString(),
                    UserName = dto.personeli.ToString(),
                    Email = dto.personeli + "@Saas.ir",
                    NormalizedEmail = dto.personeli + "@Saas.ir",
                    NormalizedUserName = dto.personeli.ToString(),
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString(),

                };
          
                var result = _userManger.CreateAsync(user, dto.Password).Result;
                if (result.Succeeded)
                {
                    var person = _mapper.Map<persons>(dto);
                    _context.Persons.Add(person);
                   var savvved= _context.SaveChanges();
                     _userManger.AddToRoleAsync(user, "User");
                    return new ResultDto<Domain.Entities.User>
                    {
                        Data = user,
                        IsSuccess = true,
                        Message = "موفق"
                    };
                }
                return new ResultDto<Domain.Entities.User>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "نا موفق رمز فاقد پیچیدگی می باشد"
                };
            }





           

        }
    }
}
