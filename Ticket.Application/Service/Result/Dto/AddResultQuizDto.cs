using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Result.Dto
{
   public class AddResultQuizDto
    {
        public long Id { get; set; }
        public int Points { get; set; }
        public string AuthorizationResult { get; set; }
        public int MaxPoints { get; set; }
        public string AnsweresInQuiz { get; set; }
        public string StudentId { get; set; }
        public DateTime StartQuiz { get; set; }
        public DateTime? EndQuiz { get; set; }
        public long QuizId { get; set; }
    }
}
