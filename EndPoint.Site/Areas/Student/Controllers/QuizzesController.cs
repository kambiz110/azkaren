using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Interfaces.Quiz;
using Azmoon.Application.Service.Quiz.Dto;
using Azmoon.Common.ResultDto;
using Azmoon.Domain.Entities;
using DNTCaptcha.Core;
using EndPoint.Site.Areas.Client.Controllers;
using EndPoint.Site.Helper.Session;
using EndPoint.Site.Useful.Ultimite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize]
    public class QuizzesController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly IQuizFacad getQuiz;
        private readonly IResultQuizFacad _resultQuiz;
        private readonly IDistributedCache distributedCache;

        public QuizzesController(UserManager<User> userManager, IQuizFacad getQuiz, IResultQuizFacad resultQuiz, IDistributedCache distributedCache)
        {
            _userManager = userManager;
            this.getQuiz = getQuiz;
            _resultQuiz = resultQuiz;
            this.distributedCache = distributedCache;
        }
        public ActionResult Index(int pageIndex = 1, int pagesize = 10)
        {


            return View(null);
        }
        [HttpPost]
        [ValidateDNTCaptcha(
            ErrorMessage = "عبارت امنیتی را به درستی وارد نمائید",
            CaptchaGeneratorLanguage = Language.Persian,
            CaptchaGeneratorDisplayMode = DisplayMode.NumberToWord)]
        public ActionResult Index(PasswordInputViewModel dto)
        {
            var result = getQuiz.getQuiz.GetQuizIdByPasswordAsync(dto.Password, dto.QuizId);
            if (!result.IsSuccess)
            {
                dto.Error = "لطفا در وارد کردن رمز عبور آزمون دقت فرمایید";
                return View(dto);
            }

            return RedirectToAction("Start", new { Id = result.Data });
        }
        public IActionResult MyQuizzes(int pageIndex = 1, int pagesize = 10)
        {

            var model = _resultQuiz.getResultQuiz.getResultByUserId(pageIndex, pagesize, 1, User.Identity.Name);

            return View(model.Data);
        }
        public IActionResult Start(string password, long quizid)
        {
            ViewBag.password = password;
            ViewBag.quizid = quizid;
            var quizModel = this.getQuiz.getQuiz.GetQuizViewStartPageById(quizid);
            if (quizModel.IsSuccess)
            {
                ViewData["quizModel"] = quizModel.Data;
            }
            return View();
        }

        [HttpGet]
        public IActionResult Submit( )
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Submit(AttemtedQuizViewModel model)
        {
          

            return this.View(model);
        }

        public IActionResult Results()
        {

            return (View());
        }
        public IActionResult StudentActiveEventsAll(int pageIndex = 1, int pagesize = 10)
        {
            var result = getQuiz.getQuizForStudendt.GetQuizes(pagesize, pageIndex, "", 1);
            return View(result.Data);
        }
        public IActionResult StudentPendingEventsAll(int pageIndex = 1, int pagesize = 10)
        {

            var result = getQuiz.getQuizForStudendt.GetQuizes(pagesize, pageIndex, "", 2);
            return View(result.Data);
        }
        public IActionResult StudentEndEventsAll(int pageIndex = 1, int pagesize = 10)
        {

            var result = getQuiz.getQuizForStudendt.GetQuizes(pagesize, pageIndex, "", 3);
            return View(result.Data);
        }

        [HttpPost]

        public IActionResult StartedQuizAjaxCall(string password, long quizid)
        {
            var userId = this._userManager.GetUserId(this.User);



            if (!String.IsNullOrEmpty(password))
            {
                var Isvalid = getQuiz.getQuiz.GetQuizIdByPasswordAsync(password, quizid).IsSuccess;
            }


            //var user = await this._userManager.GetUserAsync(this.User);
            //var roles = await this._userManager.GetRolesAsync(user);

            if (quizid > 0)
            {
                var quizModel = this.getQuiz.getQuiz.GetQuizByIdAsync(quizid).Result;

                //SessionHelper.SetObjectAsJson(HttpContext.Session, "QuizTimer", quizModel.Timer);
                //var timer = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "QuizTimer");
                var viewHtml = this.RenderViewAsync("_PartialQuizView", quizModel, true);
                return Json(new ResultDto<string>
                {
                    Data = viewHtml,
                    IsSuccess = true,
                    Message = "موفق"
                });
            }
            return Json(new ResultDto<string>
            {
                Data = "",
                IsSuccess = false,
                Message = "نا موفق"
            });

        }
    }
}
