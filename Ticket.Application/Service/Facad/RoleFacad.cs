using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Interfaces.Role;
using Azmoon.Application.Service.Role.Query;

namespace Azmoon.Application.Service.Facad
{
   public class RoleFacad : IRoleFacad
    {
        private readonly UserManager<Domain.Entities.User> _userManger;
        private readonly IMapper _mapper;
        private readonly IDataBaseContext _context;

        public RoleFacad(UserManager<Domain.Entities.User> userManger, IMapper mapper, IDataBaseContext context)
        {
            _userManger = userManger;
            _mapper = mapper;
            _context = context;
        }
        private IGetAllRolesInUser _rolesInUser;

        public IGetAllRolesInUser rolesInUser
        {
            get
            {
                return _rolesInUser = _rolesInUser ?? new GetAllRolesInUser(_userManger, _context);
            }
        }

        private IGetRoles _GetRoles;
        public IGetRoles GetRoles
        {
            get
            {
                return _GetRoles = _GetRoles ?? new GetRoles( _context);
            }
        }
    }
}
