﻿using DNTCaptcha.Core;
using EndPoint.Site.Useful.Ultimite;
using Ganss.XSS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Service.User.Dto;
using Azmoon.Common.ResultDto;
using Azmoon.Domain.Entities;
using EndPoint.Site.Useful.Static;

namespace EndPoint.Site.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IGroupFacad _workPlaceFacad;
        private readonly IUserFacad _userFacad;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IGroupFacad workPlaceFacad, IUserFacad userFacad)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _workPlaceFacad = workPlaceFacad;
            _userFacad = userFacad;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {

            return View(new LoginDto
            {
                ReturnUrl = returnUrl,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateDNTCaptcha(
            ErrorMessage = "عبارت امنیتی را به درستی وارد نمائید",
            CaptchaGeneratorLanguage = Language.Persian,
            CaptchaGeneratorDisplayMode = DisplayMode.NumberToWord)]
        public IActionResult Login(LoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var IpAddress = HttpContext.Connection.RemoteIpAddress;

            //var testIP = "10,34,99,9";
            HtmlSanitizer sanitizer = new HtmlSanitizer();

            login.UserName = sanitizer.Sanitize(login.UserName);
            login.Password = sanitizer.Sanitize(login.Password);
            var user = _userManager.FindByNameAsync(login.UserName).Result;

            _signInManager.SignOutAsync();
            if (user != null)
            {
                var result = _signInManager.PasswordSignInAsync(user, login.Password, login.IsPersistent, true).Result;
                if (result.Succeeded == true)
                {
                    var role = _userManager.GetRolesAsync(user).Result;

                    if (login.ReturnUrl != "/")
                    {
                        return Redirect(login.ReturnUrl);
                    }
                    else
                    {

                        if (role.Count < 2 && role.Contains("User"))
                        {
                            return Redirect("/Student/Quizzes/Index");
                        }
                        else
                        {
                            return Redirect("/Admin/Quiz/Index");
                        }

                    }



                }


            }
            ModelState.AddModelError(string.Empty, "نام کاربری و یا رمز عبور را به درستی وارد نمائید !!!");
            return View();
        }
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ValidateDNTCaptcha(
            ErrorMessage = "عبارت امنیتی را به درستی وارد نمائید",
            CaptchaGeneratorLanguage = Language.Persian,
            CaptchaGeneratorDisplayMode = DisplayMode.NumberToWord)]
        public IActionResult Register(ShortRegisterDto dto)
        {
            ModelState.Remove("Id");
            var validate_meki_code = dto.melli.NationalCodeValidation();
            if (!validate_meki_code.IsSuccess)
            {
                ModelState.AddModelError("melli", validate_meki_code.Message);
            }
            if (!ModelState.IsValid)
            {
                var query = from state in ModelState.Values
                            from error in state.Errors
                            select error.ErrorMessage;

                var errorList = query.ToList();
                ViewBag.Errors = errorList;
                //var workPlaceSelectListItem = _workPlaceFacad.GetWorkPlaceSelectListItem.Exequte(null).Data;
                //TempData["workPlaceSelectListItem"] = workPlaceSelectListItem;
                return View();
            }
            else
            {
                var model = _userFacad.RegisterUser.registerExqute(dto);
                if (!model.IsSuccess)
                {
                    if (model.Data == 1)
                    {
                        ModelState.AddModelError("کاربر موجود بود", model.Message);
                    }
                    if (model.Data == 0)
                    {
                        ModelState.AddModelError("کاربری در دیتابیس اولیه نبود", model.Message);
                        return RedirectToAction("CreateUser", dto);
                    }

                    var query = from state in ModelState.Values
                                from error in state.Errors
                                select error.ErrorMessage;

                    var errorList = query.ToList();
                    ViewBag.Errors = errorList;
                    //var workPlaceSelectListItem = _workPlaceFacad.GetWorkPlaceSelectListItem.Exequte(null).Data;
                    //TempData["workPlaceSelectListItem"] = workPlaceSelectListItem;
                    return View();
                }

                if (model.IsSuccess)
                {
                    var user = _userManager.FindByNameAsync(dto.personeli.ToString()).Result;
                    var SignOuted = _signInManager.SignOutAsync().IsCompleted;
                    if (SignOuted)
                    {
                        var signIned = _signInManager.PasswordSignInAsync(user, dto.Password, true
                                     , true).Result;
                    }


                    return RedirectToAction("Index", "Home");

                }
                return RedirectToAction("Index", "Home");
            }

        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult GetWorkPlaceTreeView(string name, string family)
        {
            var model = _workPlaceFacad.GetGroup.GetTreeView();
            var viewHtml = this.RenderViewAsync("_PartialWorkPlaceTreeView", model.Data, true);
            return Json(new ResultDto<string>
            {
                Data = viewHtml,
                IsSuccess = true,
                Message = "موفق"
            });
        }
        [HttpGet]
        public IActionResult CreateUser(/*ShortRegisterDto dto=null*/)
        {
            //var workPlaceSelectListItem = _workPlaceFacad.GetWorkPlaceSelectListItem.Exequte(null).Data;

            ViewData["Darajeh"] = StaticList.listeDarajeh;
            ViewData["listTypeDarajeh"] = StaticList.listTypeDarajeh;
            //if (dto!=null)
            //{
            //    var data = new RegisterUserDto { 
            //    melli=(dto.melli) ,
            //    personeli=dto.personeli,
            //    Phone=dto.Phone

            //    };
            //    return View(data);
            //}
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateDNTCaptcha(
            ErrorMessage = "عبارت امنیتی را به درستی وارد نمائید",
            CaptchaGeneratorLanguage = Language.Persian,
            CaptchaGeneratorDisplayMode = DisplayMode.NumberToWord)]
        public IActionResult CreateUser(IFormFile Image, RegisterUserDto dto, IFormCollection form)
        {
            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                var query = from state in ModelState.Values
                            from error in state.Errors
                            select error.ErrorMessage;

                var errorList = query.ToList();
                ViewBag.Errors = errorList;
                //var workPlaceSelectListItem = _workPlaceFacad.GetWorkPlaceSelectListItem.Exequte(null).Data;
                //TempData["workPlaceSelectListItem"] = workPlaceSelectListItem;
                ViewData["Darajeh"] = StaticList.listeDarajeh;
                ViewData["listTypeDarajeh"] = StaticList.listTypeDarajeh;
                return View();
            }
            else
            {
                var model = _userFacad.CreateUser.Register(dto, Image);
                if (!model.IsSuccess)
                {

                    ModelState.AddModelError("کاربر موجود بود", model.Message);
                    var query = from state in ModelState.Values
                                from error in state.Errors
                                select error.ErrorMessage;

                    var errorList = query.ToList();
                    ViewBag.Errors = errorList;
                    //var workPlaceSelectListItem = _workPlaceFacad.GetWorkPlaceSelectListItem.Exequte(null).Data;
                    //TempData["workPlaceSelectListItem"] = workPlaceSelectListItem;
                    ViewData["Darajeh"] = StaticList.listeDarajeh;
                    ViewData["listTypeDarajeh"] = StaticList.listTypeDarajeh;
                    return View();
                }

                if (model.IsSuccess)
                {
                    var user = _userManager.FindByNameAsync(dto.personeli.ToString()).Result;
                    var SignOuted = _signInManager.SignOutAsync().IsCompleted;
                    if (SignOuted)
                    {
                        var signIned = _signInManager.PasswordSignInAsync(user, dto.Password, true
                                     , true).Result;
                    }


                    return RedirectToAction("Index", "Home");

                }
                return RedirectToAction("Index", "Home");
            }
        }
    }
}