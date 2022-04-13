using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FileManagerController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Browse(string selector = "", string preview = "", string type = "")
        {
            TempData["Type"] = type;
            TempData["Selector"] = selector;
            TempData["preview"] = preview;

            return View();
        }

        public IActionResult TinyBrowse()
        {
            return View();
        }

        public IActionResult MultipleSelect()
        {
            return View();
        }
    }
}

