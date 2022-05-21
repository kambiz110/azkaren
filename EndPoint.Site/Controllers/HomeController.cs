using EndPoint.Site.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces;
using Azmoon.Application.Service.Role.Dto;
using FastReport.Web;
using EndPoint.Site.Useful.Static;
using System.IO;
using Azmoon.Application.Interfaces.Facad;
using EndPoint.Site.Useful.Ultimite;
using Azmoon.Common.ResultDto;

namespace EndPoint.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyPanel()
        {

            var cliaimesValues = User.Claims.Where(p => p.Type.Contains("role")).Select(p => p.Value).ToList();
            if (cliaimesValues!=null && cliaimesValues.Count<2 && cliaimesValues.FirstOrDefault().Where(p=>p.ToString()=="Regestered").Any())
            {
                return RedirectToAction("Index" ,"Ticket");
            }
       
            if (cliaimesValues != null)
            {
                return RedirectToAction("MyQuestion", "Question", new { area = "Admin" });
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult GetActiveQuiz(string name)
        {

            var viewHtml = this.RenderViewAsync("_PartialStudentActiveEventsAll", "", true);
            return Json(new ResultDto<string>
            {
                Data = viewHtml,
                IsSuccess = true,
                Message = "موفق"
            });
        }
    }
}
