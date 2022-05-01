﻿using Azmoon.Application.Interfaces.Facad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EndPoint.Site.Helper.ActionFilter
{
    public class SetAccessDataFilter : Microsoft.AspNetCore.Mvc.Filters.IActionFilter
    {
        private readonly IQuizFilterFacad _quizFilterFacad;

        public SetAccessDataFilter(IQuizFilterFacad quizFilterFacad)
        {
            _quizFilterFacad = quizFilterFacad;
        }

        public void OnActionExecuted(Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("quizid", out object value))
            {

                var referer = context.HttpContext.Request.Headers["Referer"].ToString();
                if (context.Controller is Microsoft.AspNetCore.Mvc.Controller controller)
                {
                  var username=  context.HttpContext.User.Identity.Name;
                    var quizId = (Int64.Parse(value.ToString()));
                   
                    if (!_quizFilterFacad.getFilter.GetAccessQuizById(quizId, username).IsSuccess)
                    {
                        //controller.ViewData["authorized"] = "unauthorized";
                        context.Result = controller.RedirectToAction("AccessDenied", "Account", new { area =""});
                    }
                  
                }
            }
        }
    }
}
