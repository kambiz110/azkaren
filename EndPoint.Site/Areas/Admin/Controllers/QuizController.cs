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

namespace EndPoint.Site.Areas.Pnl.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
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
            var result = _quizFacad.getQuiz.GetQuizes(PageSize, PageNo, q, status);
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
            TempData["categoresMentSelectListItem"] = categoresMentSelectListItem;
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

        // GET: QuizController/Edit/5
        public ActionResult Edit(long id)
        {
            var result = _quizFacad.getQuiz.GetQuizById(id);
            // var categoresMentSelectListItem = _categoreFacad.GetCategoreSelectListItem.Exequte(null).Data;
            //  TempData["categoresMentSelectListItem"] = categoresMentSelectListItem;
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
        public ActionResult Result(int PageSize, int PageNo, string q, bool status, long guizId)
        {
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
            var model = _context.Quizzes.Where(p => p.Id == id).FirstOrDefault();
            if (model != null)
            {
                ViewBag.QuizId = model.Id;
                ViewBag.QuizName = model.Name;
            }
            ViewData["listTypeDarajeh"] = StaticList.listTypeDarajeh;
            return View();
        }

        [HttpPost]
        public ActionResult Access(CreatFilterDto dto)
        {
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
                ViewData["listTypeDarajeh"] = StaticList.listTypeDarajeh;
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

        public IActionResult apiSelect2(string search)
        {
            var model = _userFacad.GetUsers.apiSelectUser(search);

            return Json(model);

        }

    }
}
