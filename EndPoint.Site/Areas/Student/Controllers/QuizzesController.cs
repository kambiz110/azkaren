using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Interfaces.Quiz;
using Azmoon.Application.Service.Quiz.Dto;
using Azmoon.Domain.Entities;
using DNTCaptcha.Core;
using EndPoint.Site.Areas.Client.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public QuizzesController(UserManager<User> userManager, IQuizFacad getQuiz, IResultQuizFacad resultQuiz)
        {
            _userManager = userManager;
            this.getQuiz = getQuiz;
            _resultQuiz = resultQuiz;
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
            var result = getQuiz.getQuiz.GetQuizIdByPasswordAsync(dto.Password);
            if (!result.IsSuccess)
            {
                dto.Error = "لطفا در وارد کردن رمز عبور آزمون دقت فرمایید";
                return View(dto);
            }

            return RedirectToAction("Start", new {Id=result.Data });
        }
        public IActionResult MyQuizzes(int pageIndex = 1, int pagesize = 10)
        {

            var model = _resultQuiz.getResultQuiz.getResultByUserId(pageIndex, pagesize, 1, User.Identity.Name);
            
            return View(model.Data);
        }
        public async Task<IActionResult> Start(string password, long id)
        {
            var Isvalid=  getQuiz.getQuiz.GetQuizIdByPasswordAsync(password ).IsSuccess;

            var user = await this._userManager.GetUserAsync(this.User);
            var roles = await this._userManager.GetRolesAsync(user);

          
            var quizModel =  this.getQuiz.getQuiz.GetQuizByIdAsync(id);
        

            return View(quizModel);
        } 
        public async Task<IActionResult> Results()
        {

            return View();
        }
    }
}
