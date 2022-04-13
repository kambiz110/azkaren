using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Service.Role.Dto;
using Azmoon.Common.ResultDto;
using Azmoon.Domain.Entities;

namespace Azmoon.Application.Service.Role.Command
{
    public class CreateRole : ICreateRole
    {
        private readonly RoleManager<Domain.Entities.Role> _roleManager;
        private readonly IMapper _mapper;
        public CreateRole( RoleManager<Domain.Entities.Role> roleManager, IMapper mapper = null)
        {
   
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public ResultDto<Domain.Entities.Role , List<IdentityError>> AddRoleExequte(AddRoleDto addRoleDto)
        {
             var newRole = _mapper.Map<Domain.Entities.Role>(addRoleDto);
            //Domain.Entities.Role newRole = new Domain.Entities.Role()
            //{
            //    Name = addRoleDto.Name,
            //    Description = addRoleDto.Description
            //};

            var result = _roleManager.CreateAsync(newRole).Result;
            if (result.Succeeded)
            {
                return new ResultDto<Domain.Entities.Role, List<IdentityError>>
                {
                TwoDate = null,
                Data = newRole,
                IsSuccess=true,
                Message="عملیات با موفقیت انجام شد."
                };
            }
            List<IdentityError> errores = result.Errors.ToList();
            return new ResultDto<Domain.Entities.Role, List<IdentityError>>
            {
                TwoDate= errores,
                Data = null,
                IsSuccess = false,
                Message = "نا موفق"
            };


        }
    }
}
