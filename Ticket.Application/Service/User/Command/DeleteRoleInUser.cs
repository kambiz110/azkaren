using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.User;
using Azmoon.Common.ResultDto;
using Microsoft.AspNetCore.Identity;

namespace Azmoon.Application.Service.User.Command
{
    public class DeleteRoleInUser : IDeleteRoleInUser
    {
        private readonly IDataBaseContext _context;
        private readonly UserManager<Domain.Entities.User> _userManger;


        public DeleteRoleInUser(IDataBaseContext context, UserManager<Domain.Entities.User> userManger)
        {
            _context = context;
            _userManger = userManger;

        }

        public ResultDto Exequte(string UserId, string RoleId, string userName)
        {
            var user = _userManger.FindByIdAsync(UserId ).Result;


            var deletionResult = _userManger.RemoveFromRoleAsync(user, RoleId).Result;
            if (deletionResult.Succeeded)
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
