using Azmoon.Application.Interfaces.Facad;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Helper.ViewComp
{
    public class GetActiveQuizViewComponent :ViewComponent
    {
        private readonly IQuizFacad _getQuiz;

        public GetActiveQuizViewComponent(IQuizFacad getQuiz)
        {
            _getQuiz = getQuiz;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _getQuiz.getQuizForStudendt.GetQuizes(100, 1, "", 1);
            var viewName = $"~/Views/Shared/Components/Quiz/{this.ViewComponentContext.ViewComponentDescriptor.ShortName}.cshtml";

            return await Task.FromResult((IViewComponentResult)View(viewName, model.Data));
        }
    }
}
