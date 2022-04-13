using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.WorkPlace;
using Azmoon.Application.Service.WorkPlace.Query;

namespace Azmoon.Application.Interfaces.Facad
{
   public interface IWorkPlaceFacad
    {
        ICreateWorkPlace CreateWork { get; }
        IGetWorkPlace GetWorkPlace { get; }
        IGetWorkPlaceSelectListItem GetWorkPlaceSelectListItem { get; }
        IGetChildrenWorkPlace GetChildrenWorkPlaces { get; }
    }
}
