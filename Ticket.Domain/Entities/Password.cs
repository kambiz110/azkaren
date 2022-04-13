using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Domain.Entities
{
    public class Password : BaseEntity
    {
        public string Content { get; set; }

        public long QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }
    }
}
