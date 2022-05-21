using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Role;
using Azmoon.Application.Service.User.Dto;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Service.Role.Query
{
    public class GetAllRolesInUser : IGetAllRolesInUser
    {
        private readonly UserManager<Domain.Entities.User> _userManger;
        private readonly RoleManager<Domain.Entities.Role> _roleManger;
        private readonly IDataBaseContext _context;

        public GetAllRolesInUser(UserManager<Domain.Entities.User> userManger, IDataBaseContext context, RoleManager<Domain.Entities.Role> roleManger)
        {
            _userManger = userManger;
            _context = context;
            _roleManger = roleManger;
        }

        public ResultDto<RolesInUserDto> Exequte(string userId)
        {
            var user = _userManger.FindByIdAsync(userId + "").Result;
          
            var roleNames = _userManger.GetRolesAsync(user).Result;

            List<Domain.Entities.Role> roless = new List<Domain.Entities.Role>();
            foreach (var item in roleNames)
            {
                roless.Add(_roleManger.FindByNameAsync(item).Result);
            }
            if (roless.Count() > 0 && roless != null)
            {
                var model = new RolesInUserDto
                {
                    UserId = user.Id,
                    FullName = $"{user.FirstName} {user.LastName}",
                    getShortRoles = new GetShortRolesForShowAdmin
                    {
                        // RolesId = roles.Select(p => p.Id).ToList(),
                        RolesId = roless.Select(p=>p.Id ).ToList(),
                        RolesName = roleNames.ToList(),
                    }
                };
                return new ResultDto<RolesInUserDto>
                {
                    Data = model,
                    IsSuccess = true,
                    Message = "موفق"
                };
            }
            else
            {
                var model2 = new RolesInUserDto
                {
                    FullName = $"{user.FirstName} {user.LastName}",
                    getShortRoles = null
                };
                return new ResultDto<RolesInUserDto>
                {
                    Data = model2,
                    IsSuccess = false,
                    Message = "ناموفق"
                };
            }


        }


    }
}
