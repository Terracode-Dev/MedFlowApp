using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedFlow.Controllers
{
    
    public class AssistantController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Patients()
        {
            return View();
        }

        public IActionResult Appointmentq()
        {
            return View();
        }

        public IActionResult Patientq()
        {
            return View();
        }
        public IActionResult Paymentq()
        {
            return View();
        }
    }
}
