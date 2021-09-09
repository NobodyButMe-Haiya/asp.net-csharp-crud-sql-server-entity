using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SqlServerDockerCSharp.Models;
using NLog;

namespace SqlServerDockerCSharp.Controllers
{
    public class HomeController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public HomeController()
        {
           
        }

        public IActionResult Index()
        {
            logger.Info("Hell You have visited the Index view" + Environment.NewLine + DateTime.Now);
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
