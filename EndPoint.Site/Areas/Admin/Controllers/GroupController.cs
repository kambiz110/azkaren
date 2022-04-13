using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Interfaces.Group;
using Azmoon.Application.Service.Group.Dto;
using Azmoon.Application.Interfaces.WorkPlace;

namespace EndPoint.Site.Areas.Administrator.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class GroupController : Controller
    {
       
        private readonly IDataBaseContext _context;
        private readonly ICreateGroup _createGroup;
        private readonly IGroupFacad _groupFacad;
        public GroupController(IDataBaseContext context, ICreateGroup createGroup, IGroupFacad groupFacad)
        {
            _context = context;
            _createGroup = createGroup;
            _groupFacad = groupFacad;
        }

        public IActionResult Index(long? parentId)
        {
            var result = _groupFacad.GetGroup.OnlyDirectChildren(parentId);
            return View(result.Data);
        }



        [HttpGet]
        public IActionResult Create()
        {
            ViewData["ParentWorkPlace"] = new SelectList(_context.Groups.Where(p => p.Status != 3), "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateGroupDto placeDto)
        {
            ModelState.Remove("Id"); ModelState.Remove("ParentId");

            if (!ModelState.IsValid)
            {
                return View(placeDto);
            }
            var result = _createGroup.Exequte(placeDto);
            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            ViewData["ParentWorkPlace"] = new SelectList(_context.Groups.Where(p => p.Status != 3), "Id", "Name");
            return View(placeDto);
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var model = _context.Groups.Where(p => p.Id == id).Select(p => new CreateGroupDto { 
            Id=p.Id ,
            Name=p.Name , 
            ParentId=p.ParentId
            }).FirstOrDefault();
            ViewData["ParentWorkPlace"] = new SelectList(_context.Groups.Where(p => p.Status != 3), "Id", "Name");

            return View("Create", model);
        }
    }
}
