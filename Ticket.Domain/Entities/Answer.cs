using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Domain.Entities
{
  public  class Answer : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public long QuestionId { get; set; }
        public Question Question { get; set; }
        public string Text { get; set; }
        public bool IsTrue { get; set; }

    }
}
