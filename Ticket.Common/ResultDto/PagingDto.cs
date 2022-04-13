using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Common.ResultDto
{
    public class PagingDto<TEntity> where TEntity : class
    {
        public PagingDto(int pageIndex, int pageSize, int count, TEntity data)
        {
            PageNo = pageIndex;
            PageSize = pageSize;
            TotalRecords = count;
            Data = data;
        }

        public PagingDto()
        {
        }

        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }

        public TEntity Data { get; set; }
    }
}
