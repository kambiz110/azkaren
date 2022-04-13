using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.User;
using Azmoon.Application.Service.User.Dto;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Service.User.Query
{
   public class GetUserForAddRole : IGetUserForAddRole
    {
        private readonly IMapper _mapper;
        private readonly RoleManager<Domain.Entities.Role> _roleManger;
        private readonly UserManager<Domain.Entities.User> _userManger;

        public GetUserForAddRole(IMapper mapper, RoleManager<Domain.Entities.Role> roleManger, UserManager<Domain.Entities.User> userManger)
        {
            _mapper = mapper;
            _roleManger = roleManger;
            _userManger = userManger;
        }

        public ResultDto<AddRoleToUserDto> Exequte(string id)
        {
            var user = _userManger.FindByIdAsync(id).Result;
            var roles = _roleManger.Roles.ToList();

            var Dto = new AddRoleToUserDto
            {
                Id = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                Roles = _mapper.Map<List<GetRoleDto>>(roles),

            };
            return new ResultDto<AddRoleToUserDto>
            {
                Data = Dto,
                IsSuccess = true,
                Message = "موفق"
            };
        }
    }
}
