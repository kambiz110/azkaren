using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Service.Answer.Dto;

namespace EndPoint.Site.Helper.ViewComp.Answer
{
    public class GetAnsweresByQuestionIdViewComponent : ViewComponent
    {
        //GetQuestionByQuizId
        public async Task<IViewComponentResult> InvokeAsync(List<GetAnswerDto> getAnswers)
        {

            var viewName = $"~/Areas/Admin/Views/Shared/Components/Answer/{this.ViewComponentContext.ViewComponentDescriptor.ShortName}.cshtml";

            return await Task.FromResult((IViewComponentResult)View(viewName, getAnswers));
        }
    }
}
