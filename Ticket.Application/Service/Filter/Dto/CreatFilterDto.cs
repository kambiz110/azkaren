using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Filter.Dto
{
   public class CreatFilterDto
    {
        [Display(Name = "محل خدمت")]
        public long WorkPlaceId { get; set; }
        public bool WorkPlaceWithChildren  { get; set; }

        public string WorkPlaceIdFake { get; set; }

        [Display(Name = "نوع درجه")]
        public  int TypeDarajeh { get; set; }

        [Display(Name = "جستجو کاربران")]
        public string UserList { get; set; }
        public long QuizId { get; set; }

    }
    public class Result
    {
        public string id { get; set; }
        public string text { get; set; }
    }

 
}
