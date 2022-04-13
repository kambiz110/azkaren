using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Helper.Hangfire
{
    public class HangfireAuthorizationFilter: IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            bool result = false;
            if (httpContext.User.Identity.IsAuthenticated && httpContext.User.IsInRole("Admin"))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return result ;
        }
    }
}
