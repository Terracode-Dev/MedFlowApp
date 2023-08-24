using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using MedFlow.Models;

namespace MedFlow.Controllers
{
    public class FileHandleController : Controller
    {

        private IWebHostEnvironment _environment;
        public int uid;
        public string uname;

        public FileHandleController (IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public IActionResult Index(Patient data)
        {
            return View();
            this.uid = data.Id;
            this.uname = data.name;
        }

        public ActionResult GeneratePrescription(string prescriptionContent)
        {
            if (string.IsNullOrEmpty(prescriptionContent))
            {
                return Content("Input is Empty.");
            }

            int userId = 123; //this.uid
            string username = "ubetta";//this.name

            string userFolderPath = Path.Combine(_environment.WebRootPath, "UserPrescriptions", userId.ToString() + "_" + username);
            string dateFolderName = DateTime.Now.ToString("yyyyMMdd");
            string dateFolderPath = Path.Combine(userFolderPath, dateFolderName);
            string fullPath = Path.Combine(dateFolderPath, "prescription.pdf");

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
            //return Content(fullPath);
            return RedirectToAction("index");
            //return RedirectToAction("Adashboard"); --move to prescription path save action in DB after saving prescription .<fullpath> data widiyata pass wenw,
            // action will redirect to doca dash...
        }
    }
}
