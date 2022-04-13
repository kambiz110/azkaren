using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Group.Dto
{
  public  class GetGroupViewModel
    {
        public long Id { get; set; }
        [Display(Name = "نام رده")]
        public string Name { get; set; }
      
        public long? ParentId { get; set; }
        [Display(Name = "وضعیت فرزند")]
        public bool IsChailren { get; set; } = false;
    }
}
