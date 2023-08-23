using MedFlow.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MedFlow.Controllers
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

        [HttpPost]

        public IActionResult Varify(login acc)
        {

            if (acc.Email == "admin@gmail.com" && acc.Password == "123")
            {
                return View("~/Views/Doctor/Doctor_dashboard.cshtml");
            }
            else
            {
                return View("~/Views/Assistant/Dashboard.cshtml");
            }

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