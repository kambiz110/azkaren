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
using Azmoon.Application.Service.Filter.Dto;

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

        public PagingDto<List<UserShowAdminDto>> Exequte(int pageIndex , int pageSize , string q)
        {
            //  var users = _userManager.Users.ToListAsync().Result;
            var users = _context.Users.AsQueryable();
            if (!String.IsNullOrEmpty(q))
            {
                users = users.Where(p => p.FirstName.Contains(q) || p.LastName.Contains(q) || p.UserName.Contains(q)).AsQueryable();
            }
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

        public ResultDto<List<Filter.Dto.Result>> apiSelectUser(string search)
        {
            //  var users = _userManager.Users.ToListAsync().Result;
            var users = _context.Users.Where(p=>p.UserName.StartsWith(search)).AsQueryable();
         
            if (users != null)
            {

                var model = _mapper.ProjectTo<Filter.Dto.Result>(users).ToList();
                if (model.Count()>0)
                {
  return new ResultDto<List<Filter.Dto.Result>>
                {
                    Data = model ,
                    IsSuccess = true,
                    Message = "موفق"

                };
                }
                else
                {
                    return new ResultDto<List<Filter.Dto.Result>>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "کاربر یافت نشد"

                    };
                }
              
            }
            return new ResultDto<List<Filter.Dto.Result>>
            {
                Data = null,
                IsSuccess=false,
                Message = "کاربر یافت نشد"

            };

        }
    }
}
