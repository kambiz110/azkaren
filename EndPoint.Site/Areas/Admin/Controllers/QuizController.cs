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

namespace EndPoint.Site.Areas.Pnl.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class QuizController : Controller
    {
        private readonly IQuizFacad _quizFacad;

        private readonly IDataBaseContext _context;
        private readonly IQuestionFacad _questionFacad;
        private readonly IResultQuizFacad _resultQuizFacad;
        private readonly ILogger<QuizController> _logger;
        private readonly IGroupFacad groupFacad;

        public QuizController(IQuizFacad quizFacad,  IDataBaseContext context, IQuestionFacad questionFacad, IResultQuizFacad resultQuizFacad, ILogger<QuizController> logger , IGroupFacad groupFacad)
        {
            _quizFacad = quizFacad;
            _context = context;
            _questionFacad = questionFacad;
            _resultQuizFacad = resultQuizFacad;
            _logger = logger;
            this.groupFacad = groupFacad;
        }

        // GET: QuizController
        public ActionResult Index(int PageSize = 10, int PageNo = 1, string q = "", string search = "", int status=1 )
        {
            if (search == "clear")
            {
                return RedirectToAction("Index");
            }
            var result = _quizFacad.getQuiz.GetQuizes( PageSize, PageNo, q, status);
            return View(result.Data);
        }

        // GET: QuizController/Details/5
        public ActionResult Details(long id ,int PageSize = 10, int PageNo = 1 )
        {
            var result = _quizFacad.getQuiz.GetQuizDetWithQuestionPagerById(id);
            var resultQus = _questionFacad.GetQuestion.GetByQuizId(PageSize ,PageNo  ,id);
            result.Data.getQuestionWithPager = resultQus.Data;
            return View(result.Data);
        }

        // GET: QuizController/Create
        public ActionResult Create()
        {
           var categoresMentSelectListItem = groupFacad.GetChildrenGroup.Exequte(null).Data;
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
            ViewBag.QuizName = model.Name;
            var result = _resultQuizFacad.getResultQuiz.getResultWithPager(PageSize,PageNo ,q ,1, guizId);
            return View(result.Data) ;
        }

  
    }
}
