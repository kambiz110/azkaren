using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.User;
using Azmoon.Application.Service.User.Dto;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Service.User.Query
{
   public class FindUser : IFindUser
    {
        private readonly IDataBaseContext _context;

        public FindUser(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<Domain.Entities.User> Exequte(string id)
        {
            var user = _context.Users.Where(p => p.Id == id).FirstOrDefault();
            if (user!=null)
            {
                return new ResultDto<Domain.Entities.User>
                {
                    Data = user,
                    IsSuccess = true,
                    Message = "موفق"
                };
            }
                return new ResultDto<Domain.Entities.User> 
                { 
                Data=null,
                IsSuccess=false,
                Message="کاربر یافت نگردید!!!!"
                };
        }

        public ResultDto<GetDitalesUserProfileDto> GetPerson(string username)
        {
            var user = _context.Users.Where(p => p.UserName == username).FirstOrDefault();
            var person = _context.Persons.Where(p=>p.personeli==user.UserName).FirstOrDefault();
            var dto = new GetDitalesUserProfileDto {
            FirstName=user.FirstName,
            LastName=user.LastName,
            darajeh=person.darajeh,
            Phone=user.Phone,
            yegan=person.yegan,
            yegan_r=person.yegan_r,
           personId=person.Id,
           userId=user.Id
            };
            return new ResultDto<GetDitalesUserProfileDto> { 
            Data=dto,
            IsSuccess=true,
            Message="موفق"
            };
        }
    }
}
