using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Domain.Entities
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public long? GroupId { get; set; }
        public Group Group { get; set; }
        public int TypeDarajeh { get; set; }
        public long? WorkPlaceId { get; set; }
        public  WorkPlace WorkPlace { get; set; }
        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

    }

}
