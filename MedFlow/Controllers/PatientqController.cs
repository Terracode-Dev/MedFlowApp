using Microsoft.AspNetCore.Mvc;
using MedFlow.Models;
using iTextSharp.xmp.impl;
using System.IO;

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

			return View(); //good to apply some error messages
            
        }

		public IActionResult updatePatient(Patient data)
		{

			
			return View("Index");
		}

        public void removeAppoinment(int aid)
        {
            var app = DbContext.appointmentq.Find(aid);
            if (app != null)
            {
                DbContext.appointmentq.Remove(app);
                DbContext.SaveChanges();
            }
        }

        public void removePrescription(int presid)
        {
            var pres = DbContext.prescriptions.Find(presid);
            Prescriptions doc;
            
            if (pres != null)
            {
                doc = DbContext.prescriptions.Where(p => p.Id == presid).FirstOrDefault();
                
                if (doc.filepath!=null)
                {
                    FileInfo presfile = new FileInfo(doc.filepath);
                    presfile.Delete();
                }
                if (doc.billfilepath!=null)
                {
                    FileInfo billfile = new FileInfo(doc.billfilepath);
                    billfile.Delete();
                }

                DbContext.prescriptions.Remove(pres);
                DbContext.SaveChanges();
            }
        }

       

            public IActionResult DeleteAppointment (int aid)
		{
			removeAppoinment (aid);
			return RedirectToAction("Index");
		}

		public IActionResult DeleteReport (int presid)
		{


            removePrescription(presid);
            return RedirectToAction("Index");
        }

        public IActionResult DownloadContent (int presid)
        {

            var pres = DbContext.prescriptions.Find(presid);
            Prescriptions doc;
            var contentType = "application/octet-stream";

            if (pres != null)
            {
                doc = DbContext.prescriptions.Where(p => p.Id == presid).FirstOrDefault();

                if (doc.filepath != null)
                {
                    var filenm = Path.GetFileName(doc.filepath);
                    return File(System.IO.File.OpenRead(doc.filepath), contentType , filenm);

                }
                if (doc.billfilepath != null)
                {
                    var file2nm = Path.GetFileName(doc.billfilepath);
                    return File(System.IO.File.OpenRead(doc.billfilepath), contentType, file2nm);
                }

                
            }




            return RedirectToAction("Index");
        }
    }
}
