using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Question.Dto
{
   public class GetPreViewQuestionDto
    {
         public long Id { get; set; }
        [Display(Name = "دپارتمان")]
        public string Department_Title { get; set; }

        [Display(Name = "موضوع")]
        public string Subject { get; set; }

        [Display(Name = "متن سوال")]
        public string Message { get; set; }
        [Display(Name = "عکس")]
        public List<string> Images { get; set; }
    }
}
