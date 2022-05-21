using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Base;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class TestController : Controller
    {


        public TestController()
        {
            StiLicense.LoadFromString("6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHkcgIvwL0jnpsDqRpWg5FI5kt2G7A0tYIcUygBh1sPs7rE7BAeUEkpkjUKhl6/j24S6yxsIWZIRjJksEoLVUjBueVKUbrngXOqKSPJ8HE3n1pShqAKcqrYW8MlF8pB4nnRnYzLWJ/P+/p8zFGywvfSWm7L6hGvJFWozdlx5wLTj4K5UuclS2XfPNkIDrt7BY5X2KVdt42NBLZbM5RdUB8iJFobpp0HzoKZI8TSn++9s0y2cM/uGn0zHRcz/b8P/PiiOJkRkm0XlFrXG19KuA6eBAUfWiHYAgTMZq2UCyOdCbDZEcF8SqCGjboFuTyI7OHTQ4PVFQY8uEmsqhes9jqiz7u7Ts7Ndy88rVAe10GiHrBdyAGf4AR4G9DFrA10fnTGIVLixX8GpNTGgsLFIOf+IQOUvdcV39PeCf2JA2vEhSqbiaiftgGwxxgbc8ENPXijj+wYztDzMBeTJUwZBheNLcD2Rqwrc//HYvbuG6aZSjPCA5DvD3QJMvdBdHM3HWvlyU0tN6xVAiECAvWQdSOks");

           
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult QuizResultReportPrint()
        {
            var key = Useful.Static.ReportConverServicese.StimulSoftKey;
            return View("QuizResultReportPrint");
        }

        public IActionResult GetReport()
        {
            StiReport report = new StiReport();
           
            var myEmployeesList = EndPoint.Site.Useful.Static.ReportConverServicese.GetEmploy();
            report.Load("wwwroot/Report/ReportTest.mrt");
            report.RegData("Employ", myEmployeesList);
            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }

    }
}
