using Microsoft.AspNetCore.Mvc;
using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Service.Question.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Helper.ViewComp.Question
{
    public class GetQuestionByQuizIdViewComponent : ViewComponent
    {
        //GetQuestionByQuizId
        public async Task<IViewComponentResult> InvokeAsync(List<GetQuestionViewModel> Questiones)
        {

            var viewName = $"~/Areas/Admin/Views/Shared/Components/Question/{this.ViewComponentContext.ViewComponentDescriptor.ShortName}.cshtml";

            return await Task.FromResult((IViewComponentResult)View(viewName, Questiones));
        }
    }
}
