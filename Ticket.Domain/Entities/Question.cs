using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Domain.Entities;

namespace Azmoon.Domain.Entities
{
    public class Question : BaseEntity
    {

        public string Text { get; set; }

        public long QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

    }
}
