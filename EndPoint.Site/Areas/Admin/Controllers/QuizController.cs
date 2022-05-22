using Ganss.XSS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Service.Quiz.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndPoint.Site.Useful.Ultimite;
using Azmoon.Common.ResultDto;
using Azmoon.Common.Useful;
using Azmoon.Application.Service.Filter.Dto;
using Stimulsoft.Base;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
//using Stimulsoft.Base;
//using Stimulsoft.Report;
//using Stimulsoft.Report.Mvc;


namespace EndPoint.Site.Areas.Pnl.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator,Teacher")]
    public class QuizController : Controller
    {
        private readonly IQuizFacad _quizFacad;
        private readonly IUserFacad _userFacad;
        private readonly IDataBaseContext _context;
        private readonly IQuestionFacad _questionFacad;
        private readonly IResultQuizFacad _resultQuizFacad;
        private readonly ILogger<QuizController> _logger;
        private readonly IGroupFacad groupFacad;
        private readonly IWorkPlaceFacad _workPlaceFacad;
        private readonly IQuizFilterFacad _quizFilterFacad;

        public QuizController(IQuizFacad quizFacad, IDataBaseContext context, IQuestionFacad questionFacad, IResultQuizFacad resultQuizFacad, ILogger<QuizController> logger, IGroupFacad groupFacad, IWorkPlaceFacad workPlaceFacad, IUserFacad userFacad, IQuizFilterFacad quizFilterFacad)
        {
            // StiLicense.LoadFromString("6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHkcgIvwL0jnpsDqRpWg5FI5kt2G7A0tYIcUygBh1sPs7koivWV0htru4Pn2682yhdY3+9jxMCVTKcKAjiEjgJzqXgLFCpe62hxJ7/VJZ9Hq5l39md0pyydqd5Dc1fSWhCtYqC042BVmGNkukYJQN0ufCozjA/qsNxzNMyEql26oHE6wWE77pHutroj+tKfOO1skJ52cbZklqPm8OiH/9mfU4rrkLffOhDQFnIxxhzhr2BL5pDFFCZ7axXX12y/4qzn5QLPBn1AVLo3NVrSmJB2KiwGwR4RL4RsYVxGScsYoCZbwqK2YrdbPHP0t5vOiLjBQ+Oy6F4rNtDYHn7SNMpthfkYiRoOibqDkPaX+RyCany0Z+uz8bzAg0oprJEn6qpkQ56WMEppdMJ9/CBnEbTFwn1s/9s8kYsmXCvtI4iQcz+RkUWspLcBzlmj0lJXWjTKMRZz+e9PmY11Au16wOnBU3NHvRc9T/Zk0YFh439GKd/fRwQrk8nJevYU65ENdAOqiP5po7Vnhif5FCiHRpxgF");
            _quizFacad = quizFacad;
            _context = context;
            _questionFacad = questionFacad;
            _resultQuizFacad = resultQuizFacad;
            _logger = logger;
            this.groupFacad = groupFacad;
            _workPlaceFacad = workPlaceFacad;
            _userFacad = userFacad;
            _quizFilterFacad = quizFilterFacad;
        }

        // GET: QuizController
        public ActionResult Index(int PageSize = 10, int PageNo = 1, string q = "", string search = "", int status = 1)
        {
            if (search == "clear")
            {
                return RedirectToAction("Index");
            }
            var result = _quizFacad.getQuiz.GetQuizes(PageSize, PageNo, q, status, User.Identity.Name);
            return View(result.Data);
        }

        // GET: QuizController/Details/5
        public ActionResult Details(long id, int PageSize = 10, int PageNo = 1)
        {
            var result = _quizFacad.getQuiz.GetQuizDetWithQuestionPagerById(id);
            var resultQus = _questionFacad.GetQuestion.GetByQuizId(PageSize, PageNo, id);
            result.Data.getQuestionWithPager = resultQus.Data;
            return View(result.Data);
        }

        // GET: QuizController/Create
        public ActionResult Create()
        {
            var categoresMentSelectListItem = groupFacad.GetGroupSelectListItem.Exequte(null).Data;
            ViewData["categoresMentSelectListItem"] = categoresMentSelectListItem;
            return View(new AddQuizDto() { }); ;
        }

        // POST: QuizController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddQuizDto dto, IFormCollection collection)
        {

            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                try
                {
                    HtmlSanitizer sanitizer = new HtmlSanitizer();

                    dto.Description = sanitizer.Sanitize(dto.Description);
                    var result = _quizFacad.addQuiz.Exequte(dto, User.Identity.Name);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    var categoresMentSelectListItem = groupFacad.GetGroupSelectListItem.Exequte(null).Data;
                    ViewData["categoresMentSelectListItem"] = categoresMentSelectListItem;
                    return View(dto);
                }
            }
            else
            {
                var categoresMentSelectListItem = groupFacad.GetGroupSelectListItem.Exequte(null).Data;
                ViewData["categoresMentSelectListItem"] = categoresMentSelectListItem;
                return View(dto);
            }


        }

        // GET: QuizController/Edit/5
        public ActionResult Edit(long id)
        {
            var result = _quizFacad.getQuiz.GetQuizById(id);
            var categoresMentSelectListItem = groupFacad.GetGroupSelectListItem.Exequte(null).Data;
            ViewData["categoresMentSelectListItem"] = categoresMentSelectListItem;

            return View(result.Data);
        }

        // POST: QuizController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddQuizDto dto, IFormCollection collection)
        {

            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                try
                {
                    HtmlSanitizer sanitizer = new HtmlSanitizer();

                    dto.Description = sanitizer.Sanitize(dto.Description);
                    var result = _quizFacad.addQuiz.Exequte(dto, User.Identity.Name);
                    return RedirectToAction("Index");
                }
                catch
                {
                    //var categoresMentSelectListItem = _categoreFacad.GetCategoreSelectListItem.Exequte(null).Data;
                    //TempData["categoresMentSelectListItem"] = categoresMentSelectListItem;
                    return View(dto);
                }
            }
            else
            {
                //var categoresMentSelectListItem = _categoreFacad.GetCategoreSelectListItem.Exequte(null).Data;
                //TempData["categoresMentSelectListItem"] = categoresMentSelectListItem;
                return View(dto);
            }
        }

        // GET: QuizController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _context.Quizzes.Where(p => p.Id == id).FirstOrDefault();
            // model.Status = false;
            model.UpdatedAt = DateTime.Now;
            _context.Quizzes.Update(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Result( string q, bool status, long guizId , int PageSize=20, int PageNo=1)
        {
            ViewBag.guizId = guizId;
            var model = _context.Quizzes.Where(p => p.Id == guizId).FirstOrDefault();
            if (model != null)
            {
                ViewBag.QuizName = model.Name;
            }

            var result = _resultQuizFacad.getResultQuiz.getResultWithPager(PageSize, PageNo, q, 1, guizId);
            return View(result.Data);
        }
        [HttpGet]
        public ActionResult Access(long id)
        {
            ViewData["listTypeDarajeh"] = StaticList.listTypeDarajeh;
            var model = _context.Quizzes.Where(p => p.Id == id).FirstOrDefault();
            if (model != null)
            {
                ViewBag.QuizId = model.Id;
                ViewBag.QuizName = model.Name;
            }
            var quizFilter = _quizFilterFacad.getFilter.GetByQuizId(id);
            if (quizFilter.IsSuccess)
            {
                return View(quizFilter.Data);
            }

            return View(new CreatFilterDto { Id = 0 });
        }

        [HttpPost]
        public ActionResult Access(CreatFilterDto dto)
        {
            ViewData["listTypeDarajeh"] = StaticList.listTypeDarajeh;
            if (String.IsNullOrEmpty(dto.UserList) && dto.WorkPlaceId == null && dto.TypeDarajeh == null)
            {
                return View(dto);
            }
            var result = _quizFilterFacad.addQuizFilter.AddFilter(dto);
            if (!result.IsSuccess)
            {
                var model = _context.Quizzes.Where(p => p.Id == dto.QuizId).FirstOrDefault();
                if (model != null)
                {
                    dto.QuizId = model.Id;
                    ViewBag.QuizId = model.Id;
                    ViewBag.QuizName = model.Name;
                }

                return View(dto);
            }

            return RedirectToAction("Index");
        }


        public IActionResult GetWorkPlaceTreeView(string name, string family)
        {
            var model = _workPlaceFacad.GetWorkPlace.GetTreeView();
            var viewHtml = this.RenderViewAsync("_PartialWorkPlaceTreeView", model.Data, true);
            return Json(new ResultDto<string>
            {
                Data = viewHtml,
                IsSuccess = true,
                Message = "موفق"
            });
        }
        public IActionResult GetGroupTreeView(string name, string family)
        {
            var model = groupFacad.GetGroup.GetTreeView();
            var viewHtml = this.RenderViewAsync("_PartialGroupTreeView", model.Data, true);
            return Json(new ResultDto<string>
            {
                Data = viewHtml,
                IsSuccess = true,
                Message = "موفق"
            });
        }
        public IActionResult apiSelect2(string search)
        {
            var model = _userFacad.GetUsers.apiSelectUser(search);

            return Json(model);

        }
        public async Task<IActionResult> DeleteFilter(long id, long quizId)
        {
            var model = await _quizFilterFacad.deleteQuizFilter.deleteFilterAsync(quizId, id);

            return Json(model);

        }


        public IActionResult AuthorizeResultQuiz(long rezultId)
        {
            var referer = this.HttpContext.Request.Headers["Referer"].ToString();
            var result = _resultQuizFacad.autorizResultIn.autorizationResultDb(rezultId);
            return Json(result);
      
        }
        public IActionResult QuizResultReportPrint()
        {
            //var key = Useful.Static.ReportConverServicese.StimulSoftKey;
            return View("QuizResultReportPrint");
        }

        public IActionResult GetReport(long quizId)
        {
            var model = _resultQuizFacad.getResultQuiz.getStimolReportQuizByQuizId(quizId ,User.Identity.Name);
            StiReport report = new StiReport();
            report.Load("wwwroot/Report/ReportQuiz.mrt");
            var myEmployeesList = EndPoint.Site.Useful.Static.ReportConverServicese.GetEmploy();
            report.RegData("QuizReport", model.Data.QuizReports);
            report["Title_Quiz"] = model.Data.Name ?? string.Empty;
            report["EndDate"] = model.Data.EndDate ?? string.Empty;
            report["StartDate"] = model.Data.StartDate ?? string.Empty;
            report["ReporNumber"] = model.Data.QuizNumber ?? string.Empty;
           
            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }

    }
}
