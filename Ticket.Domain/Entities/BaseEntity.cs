using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Domain.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public byte Status { get; set; } = 1;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public DateTime? RegesterAt { get; set; } = DateTime.Now;
    }
}
