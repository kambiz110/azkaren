using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Domain.Entities
{
   public class QuizFilter : BaseEntity
    {
        public string WorkpalceOption { get; set; }
        public int? TypeDarajeh { get; set; }
        public string UserNameOption { get; set; }
        public long QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}
