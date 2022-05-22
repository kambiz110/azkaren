using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Domain.Entities
{
    public class WorkPlace : BaseEntity
    {
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public WorkPlace Parent { get; set; }
        public int SortIndex { get; set; }
        public  ICollection<User> Users { get; set; }
    }
}
