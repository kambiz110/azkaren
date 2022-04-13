using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.WorkPlace.Dto
{
   public class GetShortDtoSelectItem
    {
        public long Id { get; set; }

        [Display(Name = "عنوان  دسته بندی")]
        public string Name { get; set; }

        [Display(Name = "والد دسته بندی")]
        public long? ParentId { get; set; }
    }
}
