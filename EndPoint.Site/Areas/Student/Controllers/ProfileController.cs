using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Facad;
using Azmoon.Common.ResultDto;
using Azmoon.Domain.Entities;
using Azmoon.Application.Service.Quiz.Dto;
using DNTCaptcha.Core;
using Azmoon.Application.Service.User.Dto;
using Azmoon.Application.Service.User.Command;
using EndPoint.Site.Useful.Static;
using EndPoint.Site.Useful.Ultimite;
using System.Security.Claims;

namespace EndPoint.Site.Areas.Client.Controllers
{
    [Area("Student")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IQuestionFacad _questionFacad;
        private readonly IQuizFacad _getQuiz;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserFacad _userFacad;
        private readonly IWorkPlaceFacad _workPlaceFacad;
        public ProfileController(UserManager<User> userManager,
            IQuestionFacad questionFacad,
                      IUserFacad userFacad,
            IQuizFacad getQuiz
            , IWorkPlaceFacad workPlaceFacad
            ,
            SignInManager<User> signInManager = null
 )
        {
            _userManager = userManager;
            _questionFacad = questionFacad;
            _getQuiz = getQuiz;
            _signInManager = signInManager;
            _userFacad = userFacad;
            _workPlaceFacad = workPlaceFacad;
        }

        // GET: ProfileController
        public ActionResult Index(int pageIndex = 1, int pagesize = 10)
        {

            //var model = _questionFacad.GetQuestion.GetByUserNamePagerClaint(User.Identity.Name ,pageIndex , pagesize);
            return View(null);
        }
        [HttpPost]
        [ValidateDNTCaptcha(
            ErrorMessage = "عبارت امنیتی را به درستی وارد نمائید",
            CaptchaGeneratorLanguage = Language.Persian,
            CaptchaGeneratorDisplayMode = DisplayMode.NumberToWord)]
        public ActionResult Index(PasswordInputViewModel dto)
        {
            var result = _getQuiz.getQuiz.GetQuizIdByPasswordAsync(dto.Password ,dto.QuizId , User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            if (!result.IsSuccess)
            {
                dto.Error = "لطفا در وارد کردن رمز عبور آزمون دقت فرمایید";
            }

            return View(dto);
        }

        [HttpGet]
        public  ActionResult GetUserDitaels()
        {
            var user =  _userFacad.FindUserById.GetPerson(User.Identity.Name);
       

            return View(user.Data);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult ChangePassword(ResetPasswordStudendtDto dto)
        {
            if (!ModelState.IsValid)
            {
                var query = from state in ModelState.Values
                            from error in state.Errors
                            select error.ErrorMessage;

                var errorList = query.ToList();
                ViewBag.Errors = errorList;

                       return View();
            }
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            _signInManager.SignOutAsync();
            var result = _signInManager.PasswordSignInAsync(user, dto.OldPassword, false, true).Result;
            if (result.Succeeded == true)
            {
                var result2 = _userManager.RemovePasswordAsync(user).Result;
                if (result2.Succeeded != true)
                {

                    var result3 = _userManager.AddPasswordAsync(user, dto.Password).Result;
                }
            }


            return RedirectToAction("GetUserDitaels");
        }
        [HttpGet]
        public IActionResult ChangeProfile()
        {
            ViewData["Darajeh"] = StaticList.listeDarajeh;
            ViewData["listTypeDarajeh"] = StaticList.listTypeDarajeh;
            var user = _userFacad.FindUserById.GetPerson(User.Identity.Name);
            return View(user.Data);
        }
        [ValidateAntiForgeryToken]
        [ValidateDNTCaptcha(
            ErrorMessage = "عبارت امنیتی را به درستی وارد نمائید",
            CaptchaGeneratorLanguage = Language.Persian,
            CaptchaGeneratorDisplayMode = DisplayMode.NumberToWord)]
        [HttpPost]
        public IActionResult ChangeProfile(GetDitalesUserProfileDto dto)
        {
            if (!ModelState.IsValid)
            {
                var query = from state in ModelState.Values
                            from error in state.Errors
                            select error.ErrorMessage;

                var errorList = query.ToList();
                ViewBag.Errors = errorList;

                ViewData["Darajeh"] = StaticList.listeDarajeh;
                ViewData["listTypeDarajeh"] = StaticList.listTypeDarajeh;
                var user = _userFacad.FindUserById.GetPerson(User.Identity.Name);
                return View(user.Data);
            }

         var result= _userFacad.updateProfile.update(dto);



            return RedirectToAction("GetUserDitaels");
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
    }
}
