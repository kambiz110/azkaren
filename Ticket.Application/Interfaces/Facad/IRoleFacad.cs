using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Role;

namespace Azmoon.Application.Interfaces.Facad
{
   public interface IRoleFacad
    {
        IGetAllRolesInUser rolesInUser { get; }
        IGetRoles GetRoles { get; }
    }
}
