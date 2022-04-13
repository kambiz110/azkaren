using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.User.Dto
{
    public class GetRoleDto
    {
        public string Id { get; set; }
        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = " {0} لطفاً وارد نمائید ")]
        public string Name { get; set; }
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = " {0} لطفاً وارد نمائید ")]
        public string Description { get; set; }

        public string userName { get; set; }
    }
}
