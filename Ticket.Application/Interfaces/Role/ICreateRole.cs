using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Service.Role.Dto;
using Azmoon.Common.ResultDto;
using Azmoon.Domain.Entities;

namespace Azmoon.Application.Interfaces
{
   public interface ICreateRole
    {
        ResultDto<Azmoon.Domain.Entities.Role , List<IdentityError>> AddRoleExequte(AddRoleDto addRoleDto);
    }
}
