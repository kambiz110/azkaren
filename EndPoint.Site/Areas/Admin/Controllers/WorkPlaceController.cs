using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Interfaces.WorkPlace;
using Azmoon.Application.Service.WorkPlace.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class WorkPlaceController : Controller
    {
       private readonly IDataBaseContext _context;

        private readonly IWorkPlaceFacad _workPlaceFacad;

        public WorkPlaceController(IDataBaseContext context,  IWorkPlaceFacad workPlaceFacad)
        {
            _context = context;

            _workPlaceFacad = workPlaceFacad;
        }

        public IActionResult Index(long? parentId)
        {
            var result = _workPlaceFacad.GetWorkPlace.OnlyDirectChildren(parentId);
            return View(result.Data);
        }



        [HttpGet]
        public IActionResult Create(long? parentId)
        {

            // ViewData["ParentWorkPlace"] = _workPlaceFacad.GetWorkPlaceSelectListItem.Exequte(parentId);
            CreateWorkPlaceDto dto = new CreateWorkPlaceDto();
            dto.Parentes = _workPlaceFacad.GetWorkPlaceSelectListItem.Exequte(parentId).Data;


            return View(dto);
        }
        [HttpPost]
        public IActionResult Create(CreateWorkPlaceDto placeDto)
        {
            ModelState.Remove("Id"); ModelState.Remove("ParentId");

            if (!ModelState.IsValid)
            {
                return View(placeDto);
            }
            var result = _workPlaceFacad.CreateWork.Exequte(placeDto);
            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            ViewData["ParentWorkPlace"] = new SelectList(_context.WorkPlaces.Where(p => p.Status != 3), "Id", "Name");
            return View(placeDto);
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var model = _context.WorkPlaces.Where(p => p.Id == id).Select(p => new CreateWorkPlaceDto
            { 
            Id=p.Id ,
            Name=p.Name , 
            ParentId=p.ParentId
            }).FirstOrDefault();
           // ViewData["ParentWorkPlace"] = new SelectList(_context.WorkPlaces.Where(p => p.Status != 3), "Id", "Name");
            model.Parentes = _workPlaceFacad.GetWorkPlaceSelectListItem.Exequte(model.ParentId).Data;
            return View("Create", model);
        }
    }
}
