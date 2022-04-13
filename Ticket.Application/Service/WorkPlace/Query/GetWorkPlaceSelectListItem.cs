using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.WorkPlace;

using Azmoon.Common.ResultDto;
using Azmoon.Application.Service.WorkPlace.Dto;

namespace Azmoon.Application.Service.WorkPlace.Query
{
   public class GetWorkPlaceSelectListItem : IGetWorkPlaceSelectListItem
    {
        private readonly IDataBaseContext _context;


        public GetWorkPlaceSelectListItem(IDataBaseContext context)
        {
            _context = context;

        }

        public ResultDto<List<SelectListItem>> Exequte(long? parentid)
        {
            List<SelectListItem> CategoryList = new List<SelectListItem>();
            List<SelectList> CategoryList2 = new List<SelectList>();
            if (parentid != null)
            {
                var Department = _context.Groups.Where(p => p.Status == 1 && p.Id == parentid)
              .AsNoTracking()
              .Select(p => new GetShortDtoSelectItem
              {
                  Id = p.Id,
                  ParentId = (p.ParentId != null) ? p.ParentId : 0,
                  Name = p.Name,
              }).FirstOrDefault();
                var optionGroup = new SelectListGroup() { Name = "والد" };
                List<SelectListItem> ffff = new List<SelectListItem>();
                ffff.Add(new SelectListItem() { Value = Department.Id.ToString(), Text = Department.Name, Group = optionGroup, Selected = true });
                return new ResultDto<List<SelectListItem>>
                {
                    Data = ffff,
                    IsSuccess = true,
                    Message = "موفق"
                };
            }
            var Categories = _context.Groups.Where(p => p.Status == 1)
                .AsNoTracking()
                .Select(p => new GetShortDtoSelectItem
                {
                    Id = p.Id,
                    ParentId = (p.ParentId != null) ? p.ParentId : 0,
                    Name = p.Name,
                }).ToList();


            var aaa = Categories
                .GroupBy(p => p.ParentId)
                .OrderBy(x => x.Key)
                 .Select(s => new
                 {
                     Key = s.Key,
                     listOfCategory = s.ToList()
                 }).ToList();


            foreach (var regionGroup in aaa)
            {
                string parentId = (regionGroup.Key > 0) ? regionGroup.Key.ToString() : null;
                var cat = "";
                if (parentId == null)
                {
                    cat = "شاخه اصلی";
                }
                else
                {
                    cat = _context.Groups.AsNoTracking().Where(o => o.Id == (Int64.Parse(parentId))).FirstOrDefault().Name;
                }
                var optionGroup = new SelectListGroup() { Name = cat };
                foreach (var city in regionGroup.listOfCategory)
                {
                    CategoryList.Add(new SelectListItem() { Value = city.Id.ToString(), Text = city.Name, Group = optionGroup });

                }
            }
            if (CategoryList.Any())
            {
                return new ResultDto<List<SelectListItem>>
                {
                    Data = CategoryList,
                    IsSuccess = true,
                    Message = "موفق"
                };
            }
            return new ResultDto<List<SelectListItem>>
            {
                Data = CategoryList,
                IsSuccess = false,
                Message = "نا موفق"
            };

        }
    }
}

