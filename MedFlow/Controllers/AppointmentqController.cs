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
        public object SearchQuery(string qname)
        {
            var dataset = DbContext.patients.Where(patient => patient.name == qname).ToList();
            return dataset;
        }

        public IActionResult Index()
        {
            return View();
        }



        public IActionResult Search(string name) {

            //url ---> Appoinmentq/Search
            var data = SearchQuery(name);
            return PartialView(data);
        }
    }
}
