﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Answer.Dto
{
   public class GetAnswerDto
    {
        public long Id { get; set; }
        [Display(Name = "متن جواب")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public string Text { get; set; }
        public long QuestionId { get; set; }
        [Display(Name = "گزینه صحیح")]
        public bool IsTrue { get; set; }
    }
}
