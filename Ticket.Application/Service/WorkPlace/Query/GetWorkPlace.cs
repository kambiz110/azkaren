using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Interfaces.WorkPlace;
using Azmoon.Application.Service.Facad;
using Azmoon.Application.Service.WorkPlace.Dto;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Service.WorkPlace.Query
{
    public class GetWorkPlace : IGetWorkPlace
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;


        public GetWorkPlace(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public ResultDto<List<GetWorkPlaceViewModel>> Execute(long? parentId)
        {
            GetChildrenWorkPlacees _getChildrenWorkPlaces = new GetChildrenWorkPlacees(_context); ;
        var result = _context.Groups.AsQueryable();
            if (parentId == null)
            {

                result = result.Where(p => p.ParentId == parentId).AsQueryable();
            }
            if (parentId!=null)
            {
                long par = (long)parentId;
                var getChildre = _getChildrenWorkPlaces.ExequteById(par);
                result = result.Where(p => getChildre.Data.Contains(p.Id) ).AsQueryable();
            }

            if (result!=null)
            {
                var model = _mapper.Map<List<GetWorkPlaceViewModel>>(result.ToList());
                for (int i = 0; i < model.Count(); i++)
                {
                    if (_context.Groups.Where(p => p.ParentId == model[i].Id).Any())
                    {
                        model[i].IsChailren = true;
                    }
                }
                return new ResultDto<List<GetWorkPlaceViewModel>>
                {
                    Data = model,
                    IsSuccess = true,
                    Message = "موفق"
                };
            }
            return new ResultDto<List<GetWorkPlaceViewModel>> 
            {
                Data = null,
                IsSuccess = false,
                Message = "ناموفق"
            };
           
        }
        public ResultDto<List<GetWorkPlaceViewModel>> GetTreeView()
        {
            GetChildrenWorkPlacees _getChildrenWorkPlaces = new GetChildrenWorkPlacees(_context); ;
            var result = _context.Groups.AsQueryable();
         

            if (result != null)
            {
                var model = _mapper.Map<List<GetWorkPlaceViewModel>>(result);
                for (int i = 0; i < model.Count(); i++)
                {
                    if (_context.Groups.Where(p => p.ParentId == model[i].Id).Any())
                    {
                        model[i].IsChailren = true;
                    }
                }
                return new ResultDto<List<GetWorkPlaceViewModel>>
                {
                    Data = model,
                    IsSuccess = true,
                    Message = "موفق"
                };
            }
            return new ResultDto<List<GetWorkPlaceViewModel>>
            {
                Data = null,
                IsSuccess = false,
                Message = "ناموفق"
            };

        }
    }
   
   
}
