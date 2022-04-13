using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.User;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Service.User.Command
{
    public class DeleteRoleInUser : IDeleteRoleInUser
    {
        private readonly IDataBaseContext _context;


        public DeleteRoleInUser(IDataBaseContext context)
        {
            _context = context;

        }

        public ResultDto Exequte(string UserId, string RoleId, string userName)
        {
           // var UserRole = _context.UserRoles.Where(p => p.RoleId == RoleId && p.UserId == UserId).AsNoTracking().FirstOrDefault();
            //if (UserRole != null)
            //{
            //   _context.UserRoles.Remove(new Domain.Entities.UserRole { RoleId = RoleId, UserId = UserId });
            //    _context.SaveChanges();
          
            //    return new ResultDto
            //    {
            //        IsSuccess = true,
            //        Message = " موفق"
            //    };
            //}
            return new ResultDto
            {
                IsSuccess = false,
                Message = "نا موفق"
            };
        }
    }
}
