using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.WorkPlace;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Service.WorkPlace.Query
{
   public class GetChildrenWorkPlacees : IGetChildrenWorkPlace
    {
        private readonly IDataBaseContext _context;

        public GetChildrenWorkPlacees(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<long>> Exequte(List<long> workplaceIdes)
        {
            List<long> resultList = new List<long>();
            resultList.AddRange(workplaceIdes);

            foreach (var item in workplaceIdes)
            {
                getChildren(item, resultList);
            }
            return new ResultDto<List<long>>
            {
                Data = resultList,
                IsSuccess = true,
                Message = "موفق"
            };
        }

        public ResultDto<List<long>> ExequteById(long? workplaceId)
        {
            var categores = _context.WorkPlaces.Where(p => p.Id == workplaceId).Select(p => p.Id).ToList();
            List<long> resultList = new List<long>();
            resultList.AddRange(categores);

            foreach (var item in categores)
            {
                getChildren(item, resultList);
            }
            return new ResultDto<List<long>>
            {
                Data = resultList,
                IsSuccess = true,
                Message = "موفق"
            };
        }
        private List<long> getChildren(long id, List<long> result)
        {

            var query = _context.WorkPlaces.Where(p => p.ParentId == id).Select(p => p.Id).ToList();
            if (query != null && query.Count() > 0)
            {
                // result.AddRange(query);
                EqulesTwoListAndAppend(query, result);
                foreach (var item in query)
                {
                    if (_context.WorkPlaces.Where(p => p.ParentId == item).Any())
                    {

                        getChildren(item, result);
                        //return result;
                    }
                }
                return result;
            }
            return result;
        }

        private List<long> EqulesTwoListAndAppend(List<long> src, List<long> dec)
        {

            foreach (var item in src)
            {
                if (!dec.Where(p => p == item).Any())
                {
                    dec.Add(item);
                }
            }
            return dec;
        }
    }
}
