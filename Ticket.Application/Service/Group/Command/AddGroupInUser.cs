using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Group;
using Azmoon.Application.Service.Group.Dto;
using Azmoon.Common.ResultDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Group.Command
{
    public class AddGroupInUser : IAddGroupInUser
    {
        private readonly IDataBaseContext _context;

        public AddGroupInUser(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<GetGroupAccessDto> Exequte(GetGroupAccessDto dto)
        {
            foreach (var item in dto.GroupIds)
            {
                if (!_context.GroupUsers.Where(p => p.GroupId == item && p.UserId == dto.UserId).AsNoTracking().Any())
                {
                    _context.GroupUsers.Add(new Domain.Entities.GroupUser() { GroupId = item, UserId = dto.UserId });
                }

            }
            var seved = _context.SaveChanges();
            if (1 > 0)
            {
                var departmentInUser = _context.GroupUsers
                    .Where(p => p.UserId == dto.UserId).AsNoTracking()
                    .Select(p => p.GroupId)
                    .ToArray();
                Boolean isArrayEqual = true;

                isArrayEqual = departmentInUser.SequenceEqual(dto.GroupIds);
                if (!isArrayEqual)
                {


                    foreach (var a in departmentInUser)
                    {
                        long resultSearch = 0;
                        foreach (var b in dto.GroupIds)
                        {
                            if (a == b)
                            {
                                resultSearch += 1;
                                break;
                            }

                        }
                        if (resultSearch > 0)
                        {
                            resultSearch = 0;
                        }
                        else
                        {
                            var DU = _context.GroupUsers.Where(p => p.GroupId == a && p.UserId == dto.UserId)
                                .AsNoTracking().FirstOrDefault();
                            if (DU != null)
                            {
                                _context.GroupUsers.Remove(DU);
                                _context.SaveChanges();
                            }

                        }
                    }


                }

            }
            return new ResultDto<GetGroupAccessDto>
            {
                Data = dto,
                IsSuccess = false,
                Message = "نا موفق"
            };
        }
    }
}
