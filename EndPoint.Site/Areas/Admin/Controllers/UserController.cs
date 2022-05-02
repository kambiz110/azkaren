using AutoMapper;
using EndPoint.Site.Useful.Ultimite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Facad;

using Azmoon.Application.Service.Question.Dto;
using Azmoon.Application.Service.User.Dto;
using Azmoon.Common.ResultDto;
using Azmoon.Domain.Entities;
using Azmoon.Application.Service.Group.Dto;
using EndPoint.Site.Useful.Static;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {
        private readonly IUserFacad _userFacad;
        private readonly IRoleFacad _roleFacad;
        private readonly IGroupFacad _groupFacad;

        private readonly UserManager<User> _userManager;

        private readonly IMapper _mapper;
        public UserController(IUserFacad userFacad, IRoleFacad roleFacad, IGroupFacad groupFacad,
           UserManager<User> userManager, IMapper mapper)
        {
            _userFacad = userFacad;
            _roleFacad = roleFacad;
            _groupFacad = groupFacad;
            _userManager = userManager;
            _mapper = mapper;
  
        }

        public IActionResult Index(int pageIndex=1, int pageSize= 10 , string q = "", string search = "")
        {
            if (search == "clear")
            {
                return RedirectToAction("Index");
            }
            var model = _userFacad.GetUsers.Exequte(pageIndex, pageSize ,q);
            return View(model);


        }
        public IActionResult AllRoleInUser(string id)
        {
            var model = _roleFacad.rolesInUser.Exequte(id).Data;
            return View(model);
        }
        [HttpGet]
        public IActionResult UpdateUser(string Id)
        {
            ViewData["Darajeh"] = StaticList.listeDarajeh;
            ViewData["listTypeDarajeh"] = StaticList.listTypeDarajeh;
            var user = _userManager.FindByIdAsync(Id).Result;
            if (user == null)
            {
                return View("Index");
            }
            var model = _mapper.Map<UpdateUserDto>(user);
            model.personeli = Int64.Parse(user.UserName);
           
            return View(model);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ValidateDNTCaptcha(
      ErrorMessage = "عبارت امنیتی را به درستی وارد نمائید",
      CaptchaGeneratorLanguage = Language.Persian,
      CaptchaGeneratorDisplayMode = DisplayMode.NumberToWord)]
        public IActionResult UpdateUser(IFormFile Image, UpdateUserDto dto, IFormCollection form , string profilePic)
        {
            ModelState.Remove("Password");
            ModelState.Remove("RePassword");
            ModelState.Remove("Image");
            if (!ModelState.IsValid)
            {
                var query = from state in ModelState.Values
                            from error in state.Errors
                            select error.ErrorMessage;

                var errorList = query.ToList();
                ViewBag.Errors = errorList;
                ViewData["Darajeh"] = StaticList.listeDarajeh;
                ViewData["listTypeDarajeh"] = StaticList.listTypeDarajeh;
                return View();
            }
            else
            {
                var user = _userManager.FindByIdAsync(dto.Id).Result;
                _mapper.Map(dto, user);
               
                var model = _userManager.UpdateAsync(user).Result;
                if (!model.Succeeded)
                {
                    ModelState.AddModelError("ExistUser","");
                    var query = from state in model.Errors
                                from error in state.Description
                                select error.ToString();

                    var errorList = query.ToList();
                    ViewBag.Errors = errorList;
                    ViewData["Darajeh"] = StaticList.listeDarajeh;
                    ViewData["listTypeDarajeh"] = StaticList.listTypeDarajeh;
                    return View(dto);
                }

         
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        public IActionResult AddRoleToUser(string id)
        {

            var result = _userFacad.GetUserForAddRole.Exequte(id);
            var roles = _roleFacad.GetRoles.Exequte().Data;
            var RolesSelectListItem = roles.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }).ToList();
            ViewData["Roles"] = RolesSelectListItem;
            return View(result.Data);
        }
        [HttpPost]
        public IActionResult AddRoleToUser([Bind("Id,Role")] AddRoleToUserDto dto)
        {
            string ip = HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
          var result =_userFacad.AddRoleToUser.Exequte(dto.Role, dto.Id, User.Identity.Name);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult UserRoles(string id)
        {
            ViewBag.UserId = id;
            var result = _roleFacad.rolesInUser.Exequte(id).Data;

            return View(result);
        }
        [HttpGet]
        [Route("Admin/User/DeleteRoleInUser/{UserId}/{RoleId}")]
        public IActionResult DeleteRoleInUser(string UserId, string RoleId)
        {
            string ip = HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var result = _userFacad.DeleteRoleInUser.Exequte(UserId, RoleId, User.Identity.Name);
            if (result.IsSuccess)
            {

            }
            var referer = HttpContext.Request.Headers["Referer"].ToString();
            return Redirect(referer);
        }

        [HttpGet]
        public IActionResult ChengeLockAccunt(string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            if (user != null)
            {
                bool locked = _userManager.GetLockoutEnabledAsync(user).Result;
                var result = _userManager.SetLockoutEnabledAsync(user, !locked).Result;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult ResetPassword(string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            if (user != null)
            {
                ResetPasswordDto dto = new ResetPasswordDto
                {
                    UserName = user.UserName,
                    FullName = user.FirstName + " " + user.LastName
                };
                return View(dto);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordDto dto)
        {
            var user = _userManager.FindByNameAsync(dto.UserName).Result;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, dto.Password);
            var result = _userManager.UpdateAsync(user).Result;
            if (result.Succeeded != true)
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GroupAccess(string Id)
        {

             var model = _groupFacad.GetGroup.GroupAccess(Id).Data;
            return View(model);
        }
        [HttpGet]
        public IActionResult EditGroupAccess(string Id)
        {

            var departMentSelectListItem = _groupFacad.GetGroupSelectListItem.Exequte(null).Data;
            ViewData["departMentSelectListItem"] = departMentSelectListItem;
           var model = _groupFacad.GetGroup.GroupAccess(Id).Data;
            return View(model);
        }
        [HttpPost]
        public IActionResult EditGroupAccess(GetGroupAccessDto dto)
        {
            var result = _groupFacad.addGroupInUser.Exequte(dto);
            var departMentSelectListItem = _groupFacad.GetGroupSelectListItem.Exequte(null).Data;
            ViewData["departMentSelectListItem"] = departMentSelectListItem;

            return RedirectToAction("GroupAccess", new { Id = dto.UserId });
        }

        [HttpGet]
        [Route("Admin/User/DeleteGroupAccess/{UserId}/{DeleteGroupAccess}")]
        public IActionResult DeleteDepartmentAccess(string UserId, long DeleteGroupAccess)
        {

            var result = _groupFacad.deleteGroupAccess.delete(UserId, DeleteGroupAccess);
            var referer = HttpContext.Request.Headers["Referer"].ToString();
            return Redirect(referer);
        }

    }
}
