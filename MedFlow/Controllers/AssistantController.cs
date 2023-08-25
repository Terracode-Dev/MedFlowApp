using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedFlow.Models;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace medflow.Controllers
{
    public class AssistantController : Controller
    {
        private readonly ApplicationDbContext DbContext;
        public AssistantController(ApplicationDbContext Context)
        {
            DbContext = Context;
        }


        //---------Utility Methods Only----

        public int CreatePatient(Patient data)
        {
            DbContext.patients.Add(data);
            DbContext.SaveChanges();
            return 1;
        }


        //----View Controllers-----

        public IActionResult SearchPatient()
        {
            return View();
        }


        //----View Transfer---
        public IActionResult Dashboard()
        {
            var ledek = new Patient
            {
                name = "saman",
                birth_date = "2002-09-18",
                contact = "0775824406",
                address = "lotus Rd",
                gender = "male",
                added_by = 1,
            };
            CreatePatient(ledek);
            return View();
        }

        public IActionResult Patients()
        {
            return View();
        }

        public IActionResult Appointmentq()
        {
            return View();
        }

        public IActionResult Patientq()
        {
            return View();
        }
    }
}

