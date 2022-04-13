using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Result.Dto
{
   public class AnswerQuizStudendt
    {
        public long QuizId { get; set; }
        public string StudentId { get; set; }
        public DateTime StartQuiz { get; set; }
        public DateTime? EndQuiz { get; set; }
        public List<QuestionStudendt> questions { get; set; }

    }
    public class QuestionStudendt {
        public long QuestionId { get; set; }
        public long AnswereId { get; set; }

    }
}
