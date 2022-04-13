using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Role.Dto
{
    public class GetShortRolesForShowAdmin
    {
        public List<string> RolesName { get; set; }
        public List<long> RolesId { get; set; }
    }
}
