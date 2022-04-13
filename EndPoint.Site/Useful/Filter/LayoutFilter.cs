using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azmoon.Common.ResultDto;
using UAParser;

namespace EndPoint.Site.Useful.Filter
{
    public class LayoutFilter : IActionFilter
    {
     

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //string ip = context.HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            //var actionName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName;
            //var controllerName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName;
            //var userAgent = context.HttpContext.Request.Headers["User-Agent"];
            //var uaParser = Parser.GetDefault();
            //ClientInfo clientInfo = uaParser.Parse(userAgent);
            //var referer = context.HttpContext.Request.Headers["Referer"].ToString();
            //var currentUrl = context.HttpContext.Request.Path;
       
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var isAdministrator = context.HttpContext.User.IsInRole(GlobalConstants.AdministratorRoleName);
                var isTeacher = context.HttpContext.User.IsInRole(GlobalConstants.TeacherRoleName);

                var controller = (Controller)context.Controller;

                if (isAdministrator || isTeacher)
                {
                    controller.ViewData["Layout"] = GlobalConstants.AdminLayout;
                }
                else
                {
                    controller.ViewData["Layout"] = GlobalConstants.StudentLayout;
                }
            }
       
    }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
