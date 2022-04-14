using AutoMapper;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.User;
using Azmoon.Application.Service.User.Dto;
using Azmoon.Common.ResultDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;

        public UpdateProfile(IDataBaseContext context, IMapper mapper = null)
        {
            _context = context;
            _mapper = mapper;
        }

        public ResultDto update(GetDitalesUserProfileDto dto)
        {
            var user = _context.Users.Where(p => p.Id == dto.userId).FirstOrDefault();
            if (user != null)
            {
                user.Id = dto.userId;
                    user.darajeh = dto.darajeh;
                    user.TypeDarajeh = dto.TypeDarajeh;
                    user.WorkPlaceId = dto.WorkPlaceId;
                    user.Phone = dto.Phone;
                
                var saved = _context.SaveChanges();
                if (saved > 0)
                {
                    return new ResultDto
                    {
                        IsSuccess = true
                    };
                }
            }



          
            return new ResultDto
            {
                IsSuccess = false
            };
        }
    }
}
