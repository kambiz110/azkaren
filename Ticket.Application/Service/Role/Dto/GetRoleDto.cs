using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Role.Dto
{
   public class GetRoleDto
    {
        public string Id { get; set; }
        [Display(Name = "نام رده")]
        public string Name { get; set; }
        [Display(Name = "رده والد")]
        public string ParentId { get; set; }
        [Display(Name = "توضیح")]
        public string Description { get; set; }

        [Display(Name = " وضعیت فرزند")]
        public bool IsChailren { get; set; } = false;
    }
}
