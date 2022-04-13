using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Role;
using Azmoon.Application.Service.Role.Dto;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Service.Role.Query
{
    public class GetRoles : IGetRoles
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetRoles(IDataBaseContext context, IMapper mapper = null)
        {
            _context = context;
            _mapper = mapper;
        }

        public ResultDto<List<GetRoleDto>> Exequte()
        {
            var IsValue = _context.Roles.Select(p => new GetRoleDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                ParentId = p.ParentId,
                IsChailren = (p.ParentId != null)
            }).AsNoTracking().ToList();
            if (IsValue != null)
            {
               // var result = _mapper.Map<List<GetRoleDto>>(IsValue);
                for (int i = 0; i < IsValue.Count; i++)
                {
                    if (_context.Roles.Where(p => p.ParentId == IsValue[i].Id).Any())
                    {
                        IsValue.ElementAt(i).IsChailren = true;
                    }
                }
                return new ResultDto<List<GetRoleDto>>
                {
                    Data = IsValue,
                    IsSuccess = true,
                    Message = "موفق"
                };
            }
            return new ResultDto<List<GetRoleDto>>
            {
                Data = null,
                IsSuccess = false,
                Message = "نا موفق"
            };
        }
     
    }
}
