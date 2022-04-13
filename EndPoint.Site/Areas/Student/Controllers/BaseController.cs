using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace EndPoint.Site.Areas.Client.Controllers
{
    public class BaseController : Controller
    {
        private IMediator _mediator;

        /// <summary>
        /// Mediator instance
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();


    }
}
