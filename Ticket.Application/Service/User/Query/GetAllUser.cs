using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.User;
using Azmoon.Application.Service.User.Dto;
using Azmoon.Common.Pagination;
using Azmoon.Common.ResultDto;


namespace Azmoon.Application.Service.User.Query
{
    public class GetAllUser : IGetAllUser
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public GetAllUser(IDataBaseContext context, IMapper mapper)
        {

            _context = context;
            _mapper = mapper;
        }

        public PagingDto<List<UserShowAdminDto>> Exequte(int pageIndex , int pageSize)
        {
            //  var users = _userManager.Users.ToListAsync().Result;
            var users = _context.Users.AsQueryable();
            int rowsCount = 0;
            if (users != null)
            {
                var pagingusers = users.ToPaged(pageIndex, pageSize, out rowsCount).ToList();
                var result = _mapper.Map<List<UserShowAdminDto>>(pagingusers);
                var data = new PagingDto<List<UserShowAdminDto>> (pageIndex, pageSize, rowsCount, result);
                return data;
            }
            return new PagingDto<List<UserShowAdminDto>>
            {
                Data = null


            };

        }
    }
}
