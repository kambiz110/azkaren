using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Group;
using Azmoon.Application.Service.Group.Query;

namespace Azmoon.Application.Interfaces.Facad
{
   public interface IGroupFacad
    {
        IGetGroup GetGroup { get; }
        IGetGroupSelectListItem GetGroupSelectListItem { get; }
        IGetChildrenGroup GetChildrenGroup { get; }
    }
}
