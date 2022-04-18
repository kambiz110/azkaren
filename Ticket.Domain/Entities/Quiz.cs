using Azmoon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Domain.Entities
{
   public class Quiz:BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int? Timer { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; } 

        public string CreatorId { get; set; }

        public virtual User Creator { get; set; }

        public long GroupId { get; set; }

        public virtual Group Group { get; set; }

        public string Password { get; set; }
        public bool IsPrivate { get; set; } = false;
        public int MaxQuestion { get; set; }
        public long? PasswordddId { get; set; }

        public virtual Password Passworddd { get; set; }
        public long? QuizFilterId { get; set; }
        public virtual QuizFilter QuizFilter { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Result> Results { get; set; }
        public ICollection<Quiz> Quizzes { get; set; }
    }
}
