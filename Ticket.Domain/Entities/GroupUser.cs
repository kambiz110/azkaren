using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Domain.Entities
{
  public  class GroupUser : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public long GroupId { get; set; }
        public Group Group { get; set; }
    }
}
