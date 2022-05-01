using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Group;
using Azmoon.Common.ResultDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Group.Command
{
    public class DeleteGroupAccess : IDeleteGroupAccess
    {
        private readonly IDataBaseContext _context;

        public DeleteGroupAccess(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto delete(string UserId, long groupId)
        {
            var UserGroup = _context.GroupUsers.Where(p => p.GroupId == groupId && p.UserId == UserId).AsNoTracking().FirstOrDefault();
            if (UserGroup != null)
            {
                _context.GroupUsers.Remove(UserGroup);
                var saved = _context.SaveChanges();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = " موفق"
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
