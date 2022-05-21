using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Interfaces.User;
using Azmoon.Application.Service.User.Command;
using Azmoon.Application.Service.User.Query;

namespace Azmoon.Application.Service.Facad
{
   public class UserFacad : IUserFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        private readonly IMapper _mapper;
        private readonly Common.FileWork.IFileProvider _fileProvider;
        private readonly RoleManager<Domain.Entities.Role> _roleManger;
        private readonly UserManager<Domain.Entities.User> _userManger;
        private readonly IGroupFacad _workPlaceFacad;


        public UserFacad(IDataBaseContext context, IHostingEnvironment environment, IMapper mapper,
            Common.FileWork.IFileProvider fileProvider, RoleManager<Domain.Entities.Role> roleManger,
            UserManager<Domain.Entities.User> userManger, IGroupFacad workPlaceFacad)
        {
            _context = context;
            _environment = environment;
            _mapper = mapper;
            _fileProvider = fileProvider;
            _roleManger = roleManger;
            _userManger = userManger;
            _workPlaceFacad = workPlaceFacad;

        }

        private IGetAllUser _getUsers;
        public IGetAllUser GetUsers
        {
            get
            {
                return _getUsers = _getUsers ?? new GetAllUser(_context, _mapper);
            }
        }

        private ICreateUser _createUser;
        public ICreateUser CreateUser 
        {
            get
            {
                return _createUser = _createUser ?? new CreateUser(_context, _fileProvider , _userManger, _roleManger ,_mapper);
            }
        }

        private IAddRoleToUser _addRoleToUser;
        public IAddRoleToUser AddRoleToUser {
            get
            {
                return _addRoleToUser = _addRoleToUser ?? new AddRoleToUser(_roleManger, _userManger);
            }
        }
        private IFindUser _FindUser;
        public IFindUser FindUserById {
            get
            {
                return _FindUser = _FindUser ?? new FindUser(_context);
            }
        }
        private IGetUserForAddRole _GetUserForAddRole;
        public IGetUserForAddRole GetUserForAddRole
        {
            get
            {
                return _GetUserForAddRole = _GetUserForAddRole ?? new GetUserForAddRole(_mapper , _roleManger, _userManger);
            }
        }
        private IDeleteRoleInUser _DeleteRoleInUser;
        public IDeleteRoleInUser DeleteRoleInUser
        {
            get
            {
                return _DeleteRoleInUser = _DeleteRoleInUser ?? new DeleteRoleInUser(_context, _userManger);
            }
        }
        private IGetChildrenUser _getChildrenUser;
        public IGetChildrenUser GetChildrenUser
        {
            get
            {
                return _getChildrenUser = _getChildrenUser ?? new GetChildrenUser(_context , _workPlaceFacad);
            }
        }
        private IRegisterUser _registerUser;
        public IRegisterUser RegisterUser
        {
            get
            {
                return _registerUser = _registerUser ?? new RegisterUser(_context, _userManger, _roleManger);
            }
        }
        private IUpdateProfile _updateProfile;
        public IUpdateProfile updateProfile
        {
            get
            {
                return _updateProfile = _updateProfile ?? new UpdateProfile(_context, _mapper);
            }
        }
    }
}
