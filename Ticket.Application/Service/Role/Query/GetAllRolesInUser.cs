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
        private readonly IDataBaseContext _context;

        public GetAllRolesInUser(UserManager<Domain.Entities.User> userManger, IDataBaseContext context)
        {
            _userManger = userManger;
            _context = context;
        }

        public ResultDto<RolesInUserDto> Exequte(string userId)
        {
            var user = _userManger.FindByIdAsync(userId + "").Result;
            var roleNames = _userManger.GetRolesAsync(user).Result;
            //var roles = _context..Where(p => p.UserId == userId).Include(p => p.Role).ToList();
            //if (roles.Count() > 0 && roles != null)
            //{
            //    var model = new RolesInUserDto
            //    {
            //        UserId = user.Id,
            //        FullName = $"{user.FirstName} {user.LastName}",
            //        getShortRoles = new GetShortRolesForShowAdmin
            //        {
            //            RolesId = roles.Select(p => p.RoleId).ToList(),
            //            RolesName = roleNames.ToList()
            //        }
            //    };
            //    return new ResultDto<RolesInUserDto>
            //    {
            //        Data = model,
            //        IsSuccess = true,
            //        Message = "موفق"
            //    };
            //}
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
