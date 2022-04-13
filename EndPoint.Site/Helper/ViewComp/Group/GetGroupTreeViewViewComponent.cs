using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Facad;

namespace EndPoint.Site.Helper.ViewComp.WorkPlace
{
    public class GetGroupTreeViewViewComponent : ViewComponent
    {
        private readonly IGroupFacad _groupFacad;

        public GetGroupTreeViewViewComponent(IGroupFacad groupFacad)
        {
            _groupFacad = groupFacad;
        }

        public async Task<IViewComponentResult> InvokeAsync(long? parentId)
        {
            var model = _groupFacad.GetGroup.Execute(parentId); ;
            var viewName = $"~/Views/Shared/Components/Group/{this.ViewComponentContext.ViewComponentDescriptor.ShortName}.cshtml";

            return await Task.FromResult((IViewComponentResult)View(viewName, model.Data));
        }
    }
}
