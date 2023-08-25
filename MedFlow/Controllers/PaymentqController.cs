using iTextSharp.text.pdf;
using iTextSharp.text;
using MedFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MedFlow.ViewModel;

namespace MedFlow.Controllers
{
    public class PaymentqController : Controller
    {
        private readonly ApplicationDbContext DbContext;
        private IWebHostEnvironment _environment;
        public int pid;
        public string uname;
        public string doctype;

        //getting the id to the globle variable 
        public int Id { get; set; }

        //Db connection/constructor
        

        public PaymentqController(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            _environment = environment;
            DbContext = context;

        }

        
        public IActionResult Index()
        {
            var dataset = DbContext.prescriptionq.ToList();
            return View(dataset);
        }


        public IActionResult addPaymentData(docModel data)
        {
            
                //save the data  at the database
                var payment = new Payments
                {
                    prescription_id = Convert.ToInt32(data.pid),
                    price = Convert.ToInt32(data.value),
                };
                DbContext.payment.Add(payment);
                DbContext.SaveChanges();

            TempData["billdata"] = data;
            

            return RedirectToAction("Index","BillFileHandle");
        }







        public IActionResult save_pay_data(docModel data)
        {
            int pid = Convert.ToInt32(data.pid);
            var pat = DbContext.patients.Find(pid);

            TempData["pid"] = data.pid;
            if (pat != null)
            {
                TempData["uname"] = pat.name;
            }
            else
            {
                TempData["uname"] = "works";
            }
            //lets see

            TempData["doctype"] = data.doc;
           //TempData["tok"] = data.tok;


            return View();

        }

       
    }
}
   



