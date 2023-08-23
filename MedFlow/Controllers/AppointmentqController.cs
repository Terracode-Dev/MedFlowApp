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
        public List<Patient> SearchQuery(string qname)
        {
            var dataset = DbContext.patients.Where(patient => patient.name == qname).ToList();
            return dataset;
        }

        public IActionResult Index()
        {
            return View();
        }



        public IActionResult Search() {

            //url ---> Appoinmentq/Search
            string searchTxt = Request.Query["searchText"];
            var data = SearchQuery(searchTxt);
            return PartialView("search",data);
        }

        public IActionResult addAppointment ()
        {
            int uid = Convert.ToInt32(Request.Query["userid"]);

            return View("index");
        }
    }
}
