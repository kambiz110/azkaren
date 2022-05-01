using Azmoon.Application.Service.Question.Dto;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Quiz.Dto
{
   public class AttemtedQuizViewModel
    {

        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Timer { get; set; }
        IEnumerable<KeyValuePair<int, string>> answer { get; set; }
        public IList<AttemtedQuizQuestionViewModel> Questions { get; set; }
    }
}
