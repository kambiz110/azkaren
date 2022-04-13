using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Interfaces.WorkPlace
{
   public interface IGetWorkPlaceSelectListItem
    {
        ResultDto<List<SelectListItem>> Exequte(long? parentid);
    }
}
