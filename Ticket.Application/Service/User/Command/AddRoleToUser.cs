using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.User;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Service.User.Command
{
    public class AddRoleToUser : IAddRoleToUser
    {
        private readonly RoleManager<Domain.Entities.Role> _roleManger;
        private readonly UserManager<Domain.Entities.User> _userManger;

        public AddRoleToUser(RoleManager<Domain.Entities.Role> roleManger, UserManager<Domain.Entities.User> userManger)
        {

            _roleManger = roleManger;
            _userManger = userManger;

        }
        public ResultDto Exequte(string roleId, string userId, string userName)
        {
            var role = _roleManger.FindByIdAsync(roleId).Result;
            var user = _userManger.FindByIdAsync(userId).Result;
            var result = _userManger.AddToRoleAsync(user, role.Name).Result;
            if (result.Succeeded)
            {
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "موفق"
                };
            }
            return new ResultDto
            {
                IsSuccess = false,
                Message = "نا موفق"
            };
        }
    }
}
