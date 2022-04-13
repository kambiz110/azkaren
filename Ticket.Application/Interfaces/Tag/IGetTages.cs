using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Azmoon.Common.ResultDto;
using Azmoon.Domain.Entities;

namespace Azmoon.Application.Interfaces.Tag
{
  public  interface IGetTages
    {

        ResultDto<List<string>> GetList(string term);

    }
}
