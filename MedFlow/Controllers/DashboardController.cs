using Microsoft.AspNetCore.Mvc;

namespace MedFlow.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
