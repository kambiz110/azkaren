using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Interfaces.Quiz;
using Azmoon.Application.Interfaces.QuizTemp;
using Azmoon.Application.Service.Filter.Dto;
using Azmoon.Application.Service.Quiz.Dto;
using Azmoon.Common.ResultDto;
using Azmoon.Domain.Entities;
using DNTCaptcha.Core;
using EndPoint.Site.Areas.Client.Controllers;
using EndPoint.Site.Helper.ActionFilter;
using EndPoint.Site.Helper.Session;
using EndPoint.Site.Useful.Ultimite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IGetQuizTemp _getQuizTemp;
        private readonly IAddQuizStartTemp _addQuizStartTemp;

        public QuizzesController(UserManager<User> userManager, IQuizFacad getQuiz, IResultQuizFacad resultQuiz, IDistributedCache distributedCache, IGetQuizTemp getQuizTemp, IAddQuizStartTemp addQuizStartTemp)
        {
            _userManager = userManager;
            this.getQuiz = getQuiz;
            _resultQuiz = resultQuiz;
            this.distributedCache = distributedCache;
            _getQuizTemp = getQuizTemp;
            _addQuizStartTemp = addQuizStartTemp;
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

        [HttpPost]
        [TypeFilter(typeof(SetAccessDataFilter))]
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
        public IActionResult Submit(AttemtedQuizViewModel model)
        {
            return this.View();
        }

        [HttpPost]

        public IActionResult Submit( IFormCollection foFormCollection , AttemtedQuizViewModel model)
        {
            HttpContext.Session.Remove("QuizTimer");
            List<KeyValuePair<string, string>> answer=new List<KeyValuePair<string, string>>();
            foreach (var item in foFormCollection)
            {
                if (item.Key!="Id" && item.Key!= "__RequestVerificationToken")
                {
                    answer.Add(new KeyValuePair<string, string> (item.Key, item.Value));
            
                }

               
            }

            return this.View();
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


            if (quizid > 0)
            {
                var quizModel = this.getQuiz.getQuiz.GetQuizByIdAsync(quizid).Result;
                var quizTemp = _getQuizTemp.GetByUserNameWithQuizId(quizid, User.Identity.Name);
                if (quizTemp.IsSuccess)
                {

                }
                if (SessionHelper.GetObjectFromJson<DateTime>(HttpContext.Session, "QuizTimer") == DateTime.MinValue)
                {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "QuizTimer", DateTime.Now.AddMinutes(quizModel.Timer));
                }
               
                var timer = SessionHelper.GetObjectFromJson<DateTime>(HttpContext.Session, "QuizTimer");
                TimeSpan span = timer.Subtract(DateTime.Now);
                if (span.Minutes<1)
                {
                    return Json(new ResultDto<string>
                    {
                        Data = "",
                        IsSuccess = false,
                        Message = "'زمان آزمون شما به پایان رسیده است!'"
                    });
                }
                quizModel.Timer = span.Minutes;
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
