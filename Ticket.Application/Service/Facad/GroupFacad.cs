using AutoMapper;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Interfaces.Group;
using Azmoon.Application.Service.Group.Query;
using Azmoon.Application.Service.Group.Command;

namespace Azmoon.Application.Service.Facad
{
    public class GroupFacad : IGroupFacad
    {
        private readonly IDataBaseContext _context;

        private readonly IMapper _mapper;


        public GroupFacad(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        private IGetGroup _getGetGroup;
        public IGetGroup GetGroup
        {
            get
            {
                return _getGetGroup = _getGetGroup ?? new GetGroup(_context, _mapper);
            }
        }
        private IGetChildrenGroup _getChildrenGroup;
        public IGetChildrenGroup GetChildrenGroup

        {
            get
            {
                return _getChildrenGroup = _getChildrenGroup ?? new GetChildrenGroup(_context);
            }

        }
        private IGetGroupSelectListItem _getGroupSelectListItem;
        public IGetGroupSelectListItem GetGroupSelectListItem
        {
            get
            {
                return _getGroupSelectListItem = _getGroupSelectListItem ?? new GetGroupSelectListItem(_context);
            }

        }
        private IAddGroupInUser _addGroupInUser;
        public IAddGroupInUser addGroupInUser
        {
            get
            {
                return _addGroupInUser = _addGroupInUser ?? new AddGroupInUser(_context);
            }

        }
        private IDeleteGroupAccess _deleteGroupAccess;
        public IDeleteGroupAccess deleteGroupAccess
        {
            get
            {
                return _deleteGroupAccess = _deleteGroupAccess ?? new DeleteGroupAccess(_context);
            }

        }
    }
}
