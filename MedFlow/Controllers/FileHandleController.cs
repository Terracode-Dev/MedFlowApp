using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using MedFlow.Models;
using MedFlow.ViewModel;

namespace MedFlow.Controllers
{
    public class FileHandleController : Controller
    {

        private IWebHostEnvironment _environment;
        private readonly ApplicationDbContext DbContext;
        public int pid;
        public string uname;
        public string doctype;

        public FileHandleController (IWebHostEnvironment environment,ApplicationDbContext context)
        {
            _environment = environment;
            DbContext = context;
        }
        public IActionResult Index(docModel data)
        {
            int pid = Convert.ToInt32(data.pid);
            var pat = DbContext.patients.Find(pid);
           
            TempData["pid"]= data.pid;
            if(pat!=null)
            {
                TempData["uname"] = pat.name;
            }
            else
            {
                TempData["uname"] = "works";
            }
            //lets see
            
            TempData["doctype"] = data.doc;
            TempData["tok"] = data.tok;


            return View();
            
        }

        public ActionResult GeneratePrescription(string prescriptionContent)
        {
            if (string.IsNullOrEmpty(prescriptionContent))
            {
                return Content("Input is Empty.");
            }

            //int userId = 123; //this.uid
            //string username = "ubetta";//this.name

            string userFolderPath = Path.Combine(_environment.WebRootPath, "UserDocuments", TempData["pid"] + "_" + TempData["uname"] );
            string dateFolderName = DateTime.Now.ToString("yyyyMMdd");
            string dateFolderPath = Path.Combine(userFolderPath, dateFolderName);
            string fullPath = Path.Combine(dateFolderPath, $"doc{TempData["doctype"]}.pdf");

            if (!Directory.Exists(userFolderPath))
            {
                Directory.CreateDirectory(userFolderPath);
            }

            if (!Directory.Exists(dateFolderPath))
            {
                Directory.CreateDirectory(dateFolderPath);
            }

            using (Document doc = new Document())
            {
                using (PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(fullPath, FileMode.Create)))
                {
                    doc.Open();

                    doc.Add(new Paragraph(prescriptionContent));

                    doc.Close();
                }
            }

            //--custom added---
            var obj = new Prescriptions
            {
                date = DateTime.Now,
                filepath = Convert.ToString(fullPath),
                patient_id = Convert.ToInt32(TempData["pid"]),
                billfilepath = "",
                payment_id=null,
                token = Convert.ToInt32(TempData["tok"]),
                

            };

            //return Content(fullPath);
            //new { pathObj = obj }
            return RedirectToAction("savePathtoDb", obj );
            //return RedirectToAction("Adashboard"); --move to prescription path save action in DB after saving prescription .<fullpath> data widiyata pass wenw,
            // action will redirect to doca dash...
        }

        public IActionResult savePathtoDb(Prescriptions pathModel)
        {
            DbContext.prescriptions.Add(pathModel);
            DbContext.SaveChanges();

            return RedirectToAction("Index", "Patientq");
        }
    }
}
