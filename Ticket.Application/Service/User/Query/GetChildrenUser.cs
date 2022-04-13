using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Interfaces.User;
using Azmoon.Application.Service.User.Dto;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Service.User.Query
{
    public class GetChildrenUser : IGetChildrenUser
    {
        private readonly IDataBaseContext _context;
        private readonly IGroupFacad _groupFacad;


        public GetChildrenUser(IDataBaseContext context, IGroupFacad groupFacad)
        {
            _context = context;
            _groupFacad = groupFacad;

        }

        public ResultDto<GetChildrenUserDto> Exequte( string userName, long[] questionId)
        {
            var user = _context.Users.AsNoTracking().Where(p => p.UserName == userName)
                .FirstOrDefault();
            var workplaces = _groupFacad.GetChildrenGroup.ExequteById(user.GroupId).Data;

            var userDitales = _context.Users.Where(p => workplaces.Contains((long)p.GroupId) && p.LockoutEnabled == true)
                .Include(p => p.Group)
                .OrderByDescending(p=>p.GroupId)
               .Select(p => new UserDitales
               {
                   CategorieName = p.Group.Name,
                   FirstName = p.FirstName,
                   LastName = p.LastName,
                   Id = p.Id
               })
                .ToList();
            var model =new GetChildrenUserDto(){ 
            QuestionId= questionId ,
            UserDitales= userDitales

            };
            return new ResultDto<GetChildrenUserDto>
            {
                Data = model,
                IsSuccess = true,
                Message = "موفق"
            };
            
        }
    }
}
