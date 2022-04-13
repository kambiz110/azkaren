using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Group;
using Azmoon.Application.Service.Group.Command;

namespace EndPoint.Site.Useful.IOC
{
    public static class IOCworkplace
    {
        public static IServiceCollection AddInjectWorkPlaceAplication(this IServiceCollection services)
        {
            services.AddScoped<ICreateGroup, CreateGroup>();
            return services;
        }
    }
}
