using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class TestController : Controller
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        public TestController(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Route("welcome")]
        public IActionResult Welcome(string userName)
        {
            var jobId = BackgroundJob.Schedule(() => SendWelcomeMail("kambiz"), TimeSpan.FromSeconds(15));
            return Ok($"Job Id {jobId} Completed. Welcome Mail Sent!");
        }
        public string testSchedule() {
            return "موفق";
        }
        public void SendWelcomeMail(string userName)
        {
            //Logic to Mail the user
            Console.WriteLine($"Welcome to our application, {userName}");
        }
    }
}
