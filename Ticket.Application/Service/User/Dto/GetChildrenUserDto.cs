using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.User.Dto
{
   public class GetChildrenUserDto
    {
        public List<UserDitales> UserDitales { get; set; }
        public long[] QuestionId { get; set; }
    }
    public class UserDitales {
        public string Id { get; set; }
        [Display(Name = "نام")]
        public string FirstName { get; set; }


        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Display(Name = "محل خدمت")]
        public string CategorieName { get; set; }
    }
}
