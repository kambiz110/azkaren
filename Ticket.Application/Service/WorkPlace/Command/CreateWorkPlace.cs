using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.WorkPlace;
using Azmoon.Application.Service.WorkPlace.Dto;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Service.WorkPlace.Command
{
    public class CreateWorkPlace : ICreateWorkPlace
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public CreateWorkPlace(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ResultDto<Domain.Entities.Group> Exequte(CreateWorkPlaceDto dto)
        {
            var wplace = _mapper.Map<Domain.Entities.Group>(dto);
            if (dto.Id>0)
            {
              //  var dbObj = _context.WorkPlaces.Where(p => p.Id == dto.Id).AsNoTracking().FirstOrDefault();
                wplace.UpdatedAt = DateTime.Now;
                wplace.Id = dto.Id;
                _context.Groups.Update(wplace);
            }
            else
            {
                wplace.RegesterAt = DateTime.Now;
                _context.Groups.Add(wplace);
            }
           
            var result = _context.SaveChangesAsync().Result;
            if (result>0)
            {
             return new ResultDto<Domain.Entities.Group>
             {
               Data=wplace,
               IsSuccess=true,
               Message="اقدام موفق بود"
                };
            }
            return new ResultDto<Domain.Entities.Group>
            {
                Data = null,
                IsSuccess = false,
                Message = "اقدام با خطا مواجه شد"
            };

        }
    }
}
