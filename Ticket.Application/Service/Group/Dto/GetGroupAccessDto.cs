using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Group.Dto
{
   public class GetGroupAccessDto
    {
        public string UserId { get; set; }

        [Display(Name = "نام , نام خانوادگی")]
        public string FullName { get; set; }

        [Display(Name = "محل خدمت")]

        public string GroupName { get; set; }

        [Display(Name = "دپارتمان")]
        [Required(ErrorMessage = " {0}  را وارد نمائید ")]
        public long[] GroupIds { get; set; }
        public string[] GroupNames { get; set; }
    }
}
