using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Domain.Entities
{
  public  class Attachment :BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string TempUrl { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public long QuestionId { get; set; }
        public Question Question { get; set; }
        public long? AnswerId { get; set; }
        public Answer Answer { get; set; }

    }
}
