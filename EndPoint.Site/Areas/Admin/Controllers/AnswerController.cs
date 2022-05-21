using EndPoint.Site.Useful.Ultimite;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Facad;

using Azmoon.Application.Service.Answer.Dto;
using Azmoon.Application.Service.Question.Dto;
using Azmoon.Common.ResultDto;
using Azmoon.Application.Interfaces.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace EndPoint.Site.Areas.Pnl.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Administrator,Teacher")]
    public class AnswerController : Controller
    {
        private readonly ILogger<AnswerController> _logger;

        private readonly IQuestionFacad _questionFacad;
        private readonly IAnswerFacad _answerFacad;
        private readonly IDataBaseContext _context;
        public AnswerController(IQuestionFacad questionFacad, IAnswerFacad answerFacad, IDataBaseContext context, ILogger<AnswerController> logger)
        {
            _questionFacad = questionFacad;
            _answerFacad = answerFacad;
            _context = context;
            _logger = logger;
        }

        public IActionResult GetAddView(long questionId)
        {
            var model =_questionFacad.GetQuestion.GetById(questionId);
            var viewHtml = this.RenderViewAsync("Partial_GetAddView", model.Data, true);
            return Json(new ResultDto<string>
            {
                Data = viewHtml,
                IsSuccess =true,
                Message = ""
            });
        }
        [HttpGet]
        public IActionResult Create(long id)
        {
            ViewBag.QuestionId = id;
            var userName = User.Identity.Name;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AddAnswerDto dto)
        {

            var userName = User.Identity.Name;
            dto.Id = 0;
            var result = _answerFacad.AddAnswer.Exequte(dto , User.Identity.Name);

            return RedirectToAction("Details", "Question", new { id = dto.QuestionId });
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var result = _answerFacad.GetAnswer.GetById(id); 
            return View(result.Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AddAnswerDto dto)
        {

            var userName = User.Identity.Name;

            var result = _answerFacad.AddAnswer.Exequte(dto, User.Identity.Name);

            return RedirectToAction("Details", "Question", new { id = dto.QuestionId });
        }

        [HttpGet]
        public IActionResult Index()
        {
            
          
            return View();
        }
        [HttpGet]
        public ActionResult Delete(long id, long qustionid)
        {
            var model = _context.Answers.Where(p => p.Id == id).FirstOrDefault();
            model.Status = 0;
            model.UpdatedAt = DateTime.Now;
            _context.Answers.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Details", "Question", new { id = qustionid });
        }
    }
}
