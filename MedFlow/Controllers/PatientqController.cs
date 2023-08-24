using Microsoft.AspNetCore.Mvc;
using MedFlow.Models;
using iTextSharp.xmp.impl;

namespace MedFlow.Controllers
{
    public class PatientqController : Controller
    {

		private readonly ApplicationDbContext DbContext;
		public PatientqController(ApplicationDbContext Context)
		{
			DbContext = Context;
		}
		public IActionResult Index()
        {
            var CAppointment = DbContext.appointmentq.Where(a => a.status == "Active").OrderBy(a => a.id).FirstOrDefault();
            
            if (CAppointment != null) {
                //var pat = DbContext.patients.Where(p => p.Id == CAppointment.patient_id).FirstOrDefault();
				var pat = DbContext.patients.Find(CAppointment.patient_id);
				if (pat != null)
				{
					var preslist = DbContext.prescriptions.Where(doc => doc.patient_id == pat.Id).ToList();
					var applist = DbContext.appointmentq.Where(app => app.patient_id==pat.Id).ToList();
					var data = new
					{
						appointment = CAppointment,
						patient = pat,
						prescription = preslist,
						apps = applist, //optionally took
					};
                    Console.WriteLine(data.patient.name);
                    return View(data);
                }
				
			}

			return View();
            
        }

		public IActionResult updatePatient(Patient data)
		{

			
			return View("Index");
		}
    }
}
