using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Domain.Entities
{
   public class Result :BaseEntity
    {
        public int Points { get; set; }

        public int MaxPoints { get; set; }


        public string AnsweresInQuiz { get; set; }
        public DateTime StartQuiz { get; set; }
        public DateTime? EndQuiz { get; set; }

        public long QuizId { get; set; }
        public virtual Quiz Quiz { get; set; }
        public string StudentId { get; set; }
        public virtual User Student { get; set; }
    }
}
