using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Service.WorkPlace.Dto;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Interfaces.WorkPlace
{
   public interface IGetWorkPlace
    {
        ResultDto<List<GetWorkPlaceViewModel>> Execute(long? parentId);
        ResultDto<List<GetWorkPlaceViewModel>> GetTreeView();
    }
}
