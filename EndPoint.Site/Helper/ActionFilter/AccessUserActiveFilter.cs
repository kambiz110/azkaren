using Azmoon.Application.Interfaces.Facad;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EndPoint.Site.Helper.ActionFilter
{
    public class AccessUserActiveFilter : IActionFilter
    {

        private readonly IQuizFacad _quizFilterFacad;

        public AccessUserActiveFilter(IQuizFacad quizFilterFacad)
        {
            _quizFilterFacad = quizFilterFacad;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("quizid", out object value))
            {
                //string password, long quizid
               
                var referer = context.HttpContext.Request.Headers["Referer"].ToString();
                if (context.Controller is Microsoft.AspNetCore.Mvc.Controller controller)
                {
                    var isPassword = context.ActionArguments.TryGetValue("password", out object pass);
                    var username = context.HttpContext.User.Identity.Name;
                    var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var quizId = (Int64.Parse(value.ToString()));
             
                    if (!_quizFilterFacad.getQuiz.GetQuizIdByPasswordAsync(isPassword ?pass.ToString():"", quizId, userId).IsSuccess)
                    {
                        //controller.ViewData["authorized"] = "unauthorized";
                        context.Result = controller.RedirectToAction("AccessDenied", "Account", new { area = ""  ,message="شما یا در این آزمون شرکت نموده اید و یا مجاز در شرکت در این آزمون نمی باشد!!"});
                    }

                }
            }
        } 
        
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
