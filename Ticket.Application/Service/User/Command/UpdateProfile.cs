using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.User;
using Azmoon.Application.Service.User.Dto;
using Azmoon.Common.ResultDto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.User.Command
{
 


    public class UpdateProfile : IUpdateProfile
    {
        private readonly IDataBaseContext _context;


        public UpdateProfile(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto update(GetDitalesUserProfileDto dto)
        {
            var user = _context.Users.Where(p=>p.Id== dto.userId).FirstOrDefault();
            var person = _context.Persons.Where(p => p.Id == dto.personId).FirstOrDefault();
            user.Phone = dto.Phone;
            person.darajeh = dto.darajeh;
            person.yegan = dto.yegan;
            person.yegan_r = dto.yegan_r;
         var saved=   _context.SaveChanges();
            if (saved>0)
            {
                return new ResultDto { 
                IsSuccess=true
                };
            }
            return new ResultDto
            {
                IsSuccess = false
            };
        }
    }
}
