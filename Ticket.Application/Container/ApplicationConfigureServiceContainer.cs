using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Azmoon.Application.Container
{
    public static class ApplicationConfigureServiceContainer
    {
        public static void AddServices(IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

        }
    }
}
