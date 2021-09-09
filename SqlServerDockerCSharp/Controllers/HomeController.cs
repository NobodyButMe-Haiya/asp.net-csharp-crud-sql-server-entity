using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqlServerDockerCSharp.Models;

namespace SqlServerDockerCSharp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Product(String notification)
        {

            ViewBag.notification = notification;
            Debug.WriteLine((string)AppDomain.CurrentDomain.GetData("ContentRootPath"));
            //ViewBag.mapPath = Path.Combine(
            //     (string)AppDomain.CurrentDomain.GetData("ContentRootPath"),
            //     "~/log.txt");

            return View();
        }
        public IActionResult Category(string notification)
        {
            ViewBag.notification = notification;
            Debug.WriteLine((string)AppDomain.CurrentDomain.GetData("ContentRootPath"));
            ViewBag.unknown = (string)AppDomain.CurrentDomain.GetData("ContentRootPath");
            // we in mysql pun in table which don't have transaction
            // sql server all table is transaction so erk ? here lol or event viewer the more proper
            //  ViewBag.mapPath = Path.Combine(
            //      (string)AppDomain.CurrentDomain.GetData("ContentRootPath"),
            //     "~/log.txt");
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
