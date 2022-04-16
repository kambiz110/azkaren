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
        public string DarajehOption { get; set; }
        public string UserNameOption { get; set; }
    }
}
