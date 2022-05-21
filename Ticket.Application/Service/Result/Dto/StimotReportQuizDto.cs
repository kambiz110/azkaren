using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Result.Dto
{
   public class StimotReportQuizDto
    {
        public List<QuizReportDro> QuizReports { get; set; }
        public string Name { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
        public string QuizNumber { get; set; }
    }
}
