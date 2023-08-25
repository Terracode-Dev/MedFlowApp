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

        //---UTILITY METHODS----
        public List<Patient> SearchQuery(string qname)
        {
            var dataset = DbContext.patients.Where(patient => patient.name == qname).ToList();
            return dataset;
        }

        public int CreateAppointment(Appointmentq data)
        {
            DbContext.appointmentq.Add(data);
            DbContext.SaveChanges();
            return 1;
        }

        public object CreateAnalyticPanel ()
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
            return assistant_obj;
        }

        //----VIEW HANDLERS----

        public IActionResult Search()
        {
            string searchTxt = Request.Query["searchText"];
            var data = SearchQuery(searchTxt);
            return PartialView("search", data);
        }


        public IActionResult Ddashboard()
        {
            

            return View(CreateAnalyticPanel());
        }

        public IActionResult addAppointment(int uid)
        {

            var data = new Appointmentq
            {
                date = DateTime.Now,
                status = "Active",
                patient_id = uid,
            };
            CreateAppointment(data);
            return RedirectToAction("Adashboard");


        }




        public IActionResult Adashboard()
        {
            return View(CreateAnalyticPanel());
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
