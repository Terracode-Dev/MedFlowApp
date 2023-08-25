using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using MedFlow.Models;
using MedFlow.ViewModel;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Org.BouncyCastle.Asn1.Ocsp;


namespace MedFlow.Controllers
{
    public class BillFileHandleController : Controller
    {

        private IWebHostEnvironment _environment;
        private readonly ApplicationDbContext DbContext;
        public int pid;
        public string uname;
        public string doctype;

        public BillFileHandleController (IWebHostEnvironment environment,ApplicationDbContext context)
        {
            _environment = environment;
            DbContext = context;
        }
        public IActionResult Index(int pqid)
        {
            prescriptionq presq = DbContext.prescriptionq.Where(e => e.id==pqid).FirstOrDefault();
            if (presq != null)
            {
                

                Patient patient = DbContext.patients.Where(p => p.Id == presq.patient_id).FirstOrDefault();

                TempData["pid"] = patient.Id;
                TempData["pname"] = patient.name;
                TempData["presId"] = presq.prescription_id;
            }
            return View();
        }

        public ActionResult GeneratePrescription(BillingModel bill)
        {
            if (string.IsNullOrEmpty(bill.billContent))
            {
                return Content("Input is Empty.");
            }

            //int userId = 123; //this.uid
            //string username = "ubetta";//this.name

            string userFolderPath = Path.Combine(_environment.WebRootPath, "UserDocuments", TempData["pid"] + "_" + TempData["pname"] );
            string dateFolderName = DateTime.Now.ToString("yyyyMMdd");
            string dateFolderPath = Path.Combine(userFolderPath, dateFolderName);
            string fullPath = Path.Combine(dateFolderPath, $"docbill.pdf");

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

                    doc.Add(new Paragraph(bill.billContent));

                    doc.Close();
                }
            }



            //---pushing to payment table--
            var slip = new Payments
            {
                prescription_id = Convert.ToInt32(TempData["presId"]),
                price = Convert.ToInt32(bill.price),
            };
            DbContext.payment.Add(slip);
            DbContext.SaveChanges();


            //--added bill saver---
            var obj = new Prescriptions
            {
                billfilepath = fullPath,
            };
            int prid = Convert.ToInt32(TempData["presId"]);
            Prescriptions docu = DbContext.prescriptions.Where(p => p.Id == prid).FirstOrDefault();
            docu.billfilepath = fullPath;
            DbContext.SaveChanges();

            //---push fullpath to prescriptions billpath colomn


            return RedirectToAction("Index", "Paymentq");
        }






        
    }
}
