using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Question.Dto
{
    public class QuestionGiveUserDto
    {
        public long[] questionId { get; set; }
        public string userId { get; set; }
    }
}
