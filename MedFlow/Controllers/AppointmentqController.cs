using MedFlow.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedFlow.Controllers
{
    public class AppointmentqController : Controller
    {

        private readonly ApplicationDbContext DbContext;
        public AppointmentqController(ApplicationDbContext Context)
        {
            DbContext = Context;
        }


        //------Utility Methods-------
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

        public List<Appointmentq> retrieveAllAppoinments ()
        {
            var dataset = DbContext.appointmentq.ToList();
            return dataset;
        }


        public void removeAppoinment (int aid)
        {
            var app = DbContext.appointmentq.Find(aid);
            if (app != null)
            {
                DbContext.appointmentq.Remove(app);
                DbContext.SaveChanges();
            }
        }

       


        //--------View Handlers-----

        public IActionResult Index()
        {
            return View(retrieveAllAppoinments());
        
            
        }
        public IActionResult Search() {

            //url ---> Appoinmentq/Search
            string searchTxt = Request.Query["searchText"];
            var data = SearchQuery(searchTxt);
            return PartialView("search",data);
        }

        public IActionResult addAppointment (int uid)
        {
            
            var data = new Appointmentq
            {
                date = DateTime.Now,
                status = "Active",
                patient_id = uid,
            };
            CreateAppointment(data);
           return View("index", retrieveAllAppoinments());

            
        }

        public IActionResult DeleteAppointment(int aid)
        {
            removeAppoinment(aid);
            return View("index", retrieveAllAppoinments());

            
        }
    }
}
