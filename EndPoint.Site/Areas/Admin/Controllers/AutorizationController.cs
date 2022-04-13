using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces;
using Azmoon.Application.Service.Role.Dto;

namespace EndPoint.Site.Areas.Administrator.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class AutorizationController : Controller
    {

        private readonly ICreateRole _createRole;

        public AutorizationController( ICreateRole createRole)
        {
        
            _createRole = createRole;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateRole()
        {

            return View();
        }
        [HttpPost]
        public IActionResult CreateRole(AddRoleDto roleDto)
        {
            var result = _createRole.AddRoleExequte(roleDto);
            if (result.IsSuccess)
            {

            }
            return View();
        }
    }
}
