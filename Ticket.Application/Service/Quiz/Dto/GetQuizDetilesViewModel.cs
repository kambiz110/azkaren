using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Quiz.Dto
{
  public  class GetQuizDetilesViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Timer { get; set; }
        public int QuestionCount { get; set; }
    }
}
