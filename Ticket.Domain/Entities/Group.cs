using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Domain.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public Group Parent { get; set; }
        public ICollection<User> Users { get; set; }
 
    }
}
