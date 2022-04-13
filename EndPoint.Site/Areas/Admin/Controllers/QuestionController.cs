using EndPoint.Site.Useful.Ultimite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Service.Question.Dto;
using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Pnl.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class QuestionController : Controller
    {
        private readonly ILogger<QuestionController> _logger;
        private readonly IQuestionFacad _questionFacad;
        private readonly IDataBaseContext _context;
        private readonly IAnswerFacad _answerFacad;
        public QuestionController(IQuestionFacad questionFacad, IDataBaseContext context, IAnswerFacad answerFacad, ILogger<QuestionController> logger)
        {
            _questionFacad = questionFacad;
            _context = context;
            _answerFacad = answerFacad;
            _logger = logger;
        }

        // GET: QuestionController
        public ActionResult Index(int PageSize = 10, int PageNo = 1, string q = "", string search = "", bool status = true)
        {
            if (search == "clear")
            {
                return RedirectToAction("Index");
            }
          //  var result = _answerFacad.GetAnswer.GetByQuestionId (PageSize, PageNo, q, status);
            return View();
        }

        // GET: QuestionController/Details/5
        public ActionResult Details(long id, int PageSize = 10, int PageNo = 1)
        {
            var result = _questionFacad.GetQuestion.GetQuestionDitelWithAnswersPager(id);
            var resultQus = _answerFacad.GetAnswer.GetByQuestionId(PageSize, PageNo, id);
            result.Data.AnswersWithPager = resultQus.Data;
            return View(result.Data);
        }
        // GET: QuestionController/Create/5
        public ActionResult Create(int id)
        {
            ViewBag.QuizId = id;
            return View();
        }
        // POST: QuestionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddQuestionViewModel dto)
        {
            dto.Id = 0;
            var result = _questionFacad.AddQuestion.Execute(dto );
          
            return RedirectToAction("Details", "Quiz",new { id= dto.QuizId});
        }
        // GET: QuestionController/Edit/5
        public ActionResult Edit(int id)
        {
            var result = _questionFacad.GetQuestion.GetById(id);
            return View(result.Data);
        }

        // POST: QuestionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddQuestionViewModel dto)
        {
            try
            {
                var result = _questionFacad.AddQuestion.Execute(dto);
                return RedirectToAction("Details", "Quiz", new { id = dto.QuizId });
            }
            catch
            {
                return View();
            }
        }

        // GET: QuestionController/Delete/5
        public ActionResult Delete(long id ,long quizid)
        {
            var model = _context.Qestions.Where(p => p.Id == id).FirstOrDefault();
            model.Status = 0;
            model.UpdatedAt = DateTime.Now;
            _context.Qestions.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Details", "Quiz", new { id = quizid });
        }

  
    }
}
