using MedFlow.Models;
using MedFlow.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace MedFlow.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext DbContext;
        public DashboardController(ApplicationDbContext Context)
        {
            DbContext = Context;
        }

        public IActionResult Adashboard()
        {
            int patient_count = DbContext.patients.Count();
            int paymentq_count = DbContext.prescriptionq.Count();
            int appointment_count = DbContext.appointmentq.Count();


            var assistant_obj = new DashboardViewModel
            {
                PatientsCount = patient_count,
                PatientqCount = appointment_count,
                PaymentqCount = paymentq_count
                

            };

            return View(assistant_obj);
        }
        [HttpPost]
        public IActionResult AddAppointment(FormCollection data)
        {
            

            return RedirectToAction("Adashboard");
        }
        [HttpPost]
        public IActionResult AddPatient(IFormCollection data)
        {
          


            var newPatient = new Patient
            {
                name = string.IsNullOrEmpty(data["name"]) ? "Null" : data["name"],
                birth_date = string.IsNullOrEmpty(data["bday"]) ? "null" : data["bday"],
                contact = string.IsNullOrEmpty(data["cno"]) ? "null" : data["cno"],
                address = string.IsNullOrEmpty(data["address"]) ? "null" : data["address"],
                added_by = 1,
                gender = "male",
            };

            DbContext.patients.Add(newPatient);
            DbContext.SaveChanges();

            return RedirectToAction("Adashboard");
        }
    }
}
