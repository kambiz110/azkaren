using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Service.Group.Dto;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Interfaces.Group
{
   public interface IGetGroup
    {
        ResultDto<List<GetGroupViewModel>> Execute(long? parentId);
        ResultDto<List<GetGroupViewModel>> GetTreeView();
        ResultDto<List<GetGroupViewModel>> OnlyDirectChildren(long? parentId);
    }
}
