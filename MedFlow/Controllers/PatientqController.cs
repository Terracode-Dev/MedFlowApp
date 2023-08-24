using Microsoft.AspNetCore.Mvc;

namespace MedFlow.Controllers
{
    public class PatientqController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
