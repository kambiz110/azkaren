using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Service.WorkPlace.Dto;
using Azmoon.Common.ResultDto;
using Azmoon.Domain.Entities;

namespace Azmoon.Application.Interfaces.WorkPlace
{
   public interface ICreateWorkPlace
    {
        ResultDto<Domain.Entities.Group> Exequte(CreateWorkPlaceDto dto);
    }
}
