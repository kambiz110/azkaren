using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Common.ResultDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.WorkPlace.Query
{
   public interface IGetWorkplacFirstToEndParent
    {
        ResultDto<string> FirstToEndParent(long workplaceId);
    }
    public class GetWorkplacFirstToEndParent : IGetWorkplacFirstToEndParent
    {
        private readonly IDataBaseContext _context;

        public GetWorkplacFirstToEndParent(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<string> FirstToEndParent(long workplaceId)
        {
          var  srtResult = "";
          var  counter = 0;
            var category= _context.WorkPlaces.Where(p => p.Id == workplaceId).FirstOrDefault();
            List<string> resultList = new List<string>();
          

           if (category != null)
            {
                resultList.Add(category.Name);
                if (category.ParentId!=null)
                {
 getChildren((long)category.ParentId, resultList);
                }
               
                for (int i =resultList.Count()-1 ; i >= 0; i--)
                {
          
                    if (counter== resultList.Count()-1)
                    {
                        srtResult = srtResult + resultList.ElementAt(i).Trim() ;
                    }
                    else
                    {
                        srtResult = srtResult + resultList.ElementAt(i).Trim() + "/";
                    }
                    counter++;
                }
                return new ResultDto<string>
                {
                    Data = srtResult,
                    IsSuccess = true,
                    Message = " موفق"
                };
            }
            return new ResultDto<string>
            {
                Data = srtResult,
                IsSuccess = false,
                Message = "نا موفق"
            };
           
        }
        private List<string> getChildren(long id, List<string> result)
        {

            var query = _context.WorkPlaces.AsNoTracking().Where(p => p.Id == id).FirstOrDefault();
            if (query != null )
            {
              
               
                    // result.AddRange(query);
                    if (!result.Where(p => p == query.Name).Any())
                    {
                        result.Add(query.Name);
                    }
                    if (_context.WorkPlaces.Where(p => p.Id == query.ParentId).Any())
                    {

                        getChildren((long)query.ParentId, result);
                        //return result;
                    }
              
                return result;
            }
            return result;
        }
    }
}
