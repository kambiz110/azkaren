using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Quiz.Dto
{
   public class GetQuizForStudendtDto
    {
        public long QuizId { get; set; }
        public string QuizName { get; set; }
        public List<QustionesInQuiz> qustiones { get; set; }

    }


    public class QustionesInQuiz
    {
        public long QuestionId { get; set; }
        public string QustionText { get; set; }
        public List<AnswerInQustion> answers { get; set; }
    }


    public class AnswerInQustion
    {
        public long AnswereId { get; set; }
        public string Text { get; set; }
        public bool IsTrue { get; set; }
    }


}
