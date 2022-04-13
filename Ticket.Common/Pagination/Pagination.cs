using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Common.Pagination
{
    public static class Pagination
    {

        public static IEnumerable<TSource> ToPaged<TSource>(this IEnumerable<TSource> source, int page, int pageSize, out int rowsCount)
        {
            if (source != null)
            {
                rowsCount = source.Count();
            }
            else
            {
                rowsCount = 0;
            }

            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
