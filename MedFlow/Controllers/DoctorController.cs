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
    public class DoctorController : Controller
    {
        
        private readonly ApplicationDbContext DbContext;
        public DoctorController(ApplicationDbContext Context)
        {
            DbContext = Context;
        }

        public IActionResult Index()
        {


            return View();
        }
    }
}

