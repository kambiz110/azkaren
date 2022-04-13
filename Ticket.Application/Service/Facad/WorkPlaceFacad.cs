using AutoMapper;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Interfaces.WorkPlace;
using Azmoon.Application.Service.WorkPlace.Query;
using Azmoon.Application.Service.WorkPlace.Command;

namespace Azmoon.Application.Service.Facad
{
   public class WorkPlaceFacad : IWorkPlaceFacad
    {
        private readonly IDataBaseContext _context;

        private readonly IMapper _mapper;


        public WorkPlaceFacad(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
 
        }

        private IGetWorkPlace _getGetWorkPlace;
        public IGetWorkPlace GetWorkPlace
        {
            get
            {
                return _getGetWorkPlace = _getGetWorkPlace ?? new GetWorkPlace(_context , _mapper );
            }
        }
        private IGetWorkPlaceSelectListItem _getWorkPlaceSelectListItem;
        public IGetWorkPlaceSelectListItem GetWorkPlaceSelectListItem

        {
            get
            {
                return _getWorkPlaceSelectListItem = _getWorkPlaceSelectListItem ?? new GetWorkPlaceSelectListItem(_context);
            }

        }
        private IGetChildrenWorkPlace _getChildrenWorkPlaces;
        public IGetChildrenWorkPlace GetChildrenWorkPlaces
        {
            get
            {
                return _getChildrenWorkPlaces = _getChildrenWorkPlaces ?? new GetChildrenWorkPlacees(_context);
            }

        }
        private ICreateWorkPlace _CreateWorkPlace;
        public ICreateWorkPlace CreateWork
        {
            get
            {
                return _CreateWorkPlace = _CreateWorkPlace ?? new CreateWorkPlace(_context ,_mapper);
            }

        }
    }
}
