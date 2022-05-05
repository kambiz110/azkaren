using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Interfaces.Quiz;
using Azmoon.Application.Interfaces.QuizTemp;
using Azmoon.Application.Service.Filter.Dto;
using Azmoon.Application.Service.Quiz.Dto;
using Azmoon.Application.Service.QuizTemp.Dto;
using Azmoon.Application.Service.Result.Dto;
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
using System.Security.Claims;
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
        [ValidateAntiForgeryToken]
        [ValidateDNTCaptcha(
            ErrorMessage = "عبارت امنیتی را به درستی وارد نمائید",
            CaptchaGeneratorLanguage = Language.Persian,
            CaptchaGeneratorDisplayMode = DisplayMode.NumberToWord)]
        public ActionResult Index(PasswordInputViewModel dto)
        {
            if (ModelState.IsValid)
            {
                var result = getQuiz.getQuiz.GetQuizIdByPasswordAsync(dto.Password, dto.QuizId, User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
                if (!result.IsSuccess)
                {
                    dto.Error = "لطفا در وارد کردن رمز عبور و کد آزمون دقت فرمایید";
                    return View(dto);
                }
                else
                {
                    return RedirectToAction("Start", new { password= dto.Password,Iquizidd = result.Data });
                }
            }

            dto.Error = "لطفا در وارد کردن رمز عبور و کد آزمون دقت فرمایید";
            return View(dto);

        }
        public IActionResult MyQuizzes(int pageIndex = 1, int pagesize = 10)
        {

            var model = _resultQuiz.getResultQuiz.getResultByUserId(pageIndex, pagesize, 1, User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View(model.Data);
        }

        [HttpPost]
        [TypeFilter(typeof(AccessUserActiveFilter))]
        [TypeFilter(typeof(SetAccessFilter))]
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



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(IFormCollection foFormCollection)
        {
            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            HttpContext.Session.Remove("QuizTimer");
            List<KeyValuePair<string, string>> answer = new List<KeyValuePair<string, string>>();
            var id = foFormCollection.Where(p => p.Key == "Id").FirstOrDefault().Value;
            foreach (var item in foFormCollection)
            {

                if (item.Key.StartsWith("answer"))
                {
                    var Key = (item.Key).Split('_');
                    answer.Add(new KeyValuePair<string, string>(Key[1], item.Value));

                }


            }
            DataResultQuizDto dto = new DataResultQuizDto { QuizId = Int64.Parse(id), answer = answer, username = User.Identity.Name ,Ip=ip };
            var result = _resultQuiz.addResultQuiz.addResultQuiz(dto);
            return Redirect("/Student/Quizzes/Index");
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
            var ip = Request.HttpContext.Connection.RemoteIpAddress .ToString();
            if (quizid > 0)
            {
                var QuizTemp = new AddQuizTempDto();
                var quizModel = this.getQuiz.getQuiz.GetQuizByIdAsync(quizid).Result;
                var getQuizTemp = _getQuizTemp.GetByUserNameWithQuizId(quizid, User.Identity.Name);
                if (!getQuizTemp.IsSuccess && getQuizTemp.Message != "Bad_Request")
                {
                    var resAddTemp = _addQuizStartTemp.Add(DateTime.Now, quizid, User.Identity.Name ,ip);
                    if (resAddTemp.IsSuccess)
                    {
                        QuizTemp = resAddTemp.Data;
                    }
                    else
                    {
                        return Json(new ResultDto<string>
                        {
                            Data = "",
                            IsSuccess = false,
                            Message = "نا موفق"
                        });
                    }
                }
                else
                {
                    QuizTemp = getQuizTemp.Data;
                }


                TimeSpan span = QuizTemp.EndDate.AddMinutes(1).Subtract(DateTime.Now);
                if (span.Minutes < 1)
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
