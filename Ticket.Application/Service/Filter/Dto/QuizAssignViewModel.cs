﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Filter.Dto
{
    public class QuizAssignViewModel 
    {
        public string Id { get; set; }
        [Display(Name = "نام آزمون")]
        public string Name { get; set; }

        public string CreatorId { get; set; }

        public bool IsAssigned { get; set; }
        [Display(Name = "تاریخ و زمان شروع")]
        public DateTime StartDate { get; set; }

        [Display(Name = "تاریخ و زمان پایان")]
        public DateTime EndDate { get; set; }
    }
}
