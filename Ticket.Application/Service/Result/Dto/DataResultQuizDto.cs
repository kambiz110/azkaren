using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Result.Dto
{
  public  class DataResultQuizDto
    {
        public IEnumerable<KeyValuePair<string, string>> answer { get; set; }
        public long QuizId { get; set; }
        public string username { get; set; }
    }
}
