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

        public IActionResult Index(int pageIndex=1, int pageSize=10)
        {
            var model = _userFacad.GetUsers.Exequte(pageIndex, pageSize);
            return View(model);


        }
        public IActionResult AllRoleInUser(string id)
        {
            var model = _roleFacad.rolesInUser.Exequte(id).Data;
            return View(model);
        }
        public IActionResult Update(string Id)
        {
            var user = _userManager.FindByIdAsync(Id).Result;
            if (user==null)
            {
                return View("Index");
            }
            var model = _mapper.Map<RegisterUserDto>(user);
            model.personeli = Int64.Parse( user.UserName);
            var workPlaceSelectListItem = _groupFacad.GetGroupSelectListItem.Exequte(null).Data;
            TempData["workPlaceSelectListItem"] = workPlaceSelectListItem;
            return View(model);
        }
        [HttpPost]
        public IActionResult Update(IFormFile Image, RegisterUserDto dto, IFormCollection form , string profilePic)
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
                var workPlaceSelectListItem = _groupFacad.GetGroupSelectListItem.Exequte(null).Data;
                TempData["workPlaceSelectListItem"] = workPlaceSelectListItem;
                return View();
            }
            else
            {
                var model = _userFacad.CreateUser.Register(dto, Image);
                if (!model.IsSuccess)
                {
                    ModelState.AddModelError("ExistUser", model.Message);
                    var query = from state in ModelState.Values
                                from error in state.Errors
                                select error.ErrorMessage;

                    var errorList = query.ToList();
                    ViewBag.Errors = errorList;
                    var workPlaceSelectListItem = _groupFacad.GetGroupSelectListItem.Exequte(null).Data;
                    TempData["workPlaceSelectListItem"] = workPlaceSelectListItem;
                    ViewBag.profilePic = profilePic;
                    return View(dto);
                }

                if (model.IsSuccess)
                {
              


                    return RedirectToAction("Index");

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




   
    }
}
