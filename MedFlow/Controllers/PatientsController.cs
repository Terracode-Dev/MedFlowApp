using MedFlow.Models;
using MedFlow.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedFlow.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext DbContext;

        //Db connection/constructor
        public PatientsController(ApplicationDbContext Context)
        {
            DbContext = Context;
        }

        //patient tab load
        public IActionResult Index()
        {
            IEnumerable<Patient> patientList = DbContext.patients;
            return View(patientList);
        }

        //search patient
        [HttpGet]
        public IActionResult SearchPatient(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return RedirectToAction("Index");
            }

            IEnumerable<Patient> filteredPatients = DbContext.patients.Where(patient => patient.name.Contains(searchString) || patient.Id.ToString() == searchString);

            return View("Index", filteredPatients);
        }

        //add patient
        [HttpPost]
        public IActionResult AddPatient(IFormCollection data)
        {



            var newPatient = new Patient
            {
                name = string.IsNullOrEmpty(data["name"]) ? "Null" : data["name"],
                birth_date = string.IsNullOrEmpty(data["bday"]) ? "null" : data["bday"],
                contact = string.IsNullOrEmpty(data["cno"]) ? "null" : data["cno"],
                address = string.IsNullOrEmpty(data["address"]) ? "null" : data["address"],
                added_by = 1,                                                                   //add role............!
                gender = string.IsNullOrEmpty(data["gender"]) ? "null" : data["gender"],
            };

            DbContext.patients.Add(newPatient);
            DbContext.SaveChanges();

            return RedirectToAction("index");
        }

        //edit btn
        public IActionResult EditPatient(int? Id)
        {
            if(Id == null || Id == 0)
            { 
                return NotFound();
            }

            var dataPatient = DbContext.patients.Find(Id);

            if(dataPatient == null)
            {
                return NotFound();
            }

            var reports = DbContext.prescriptions.Where(ph => ph.Id == Id).ToList();

            var newdata = new editPatientViewModel
            { 
                Patient = dataPatient,
                prescriptions = reports
            };
            return View(newdata); 
        }

        //edit function

        [HttpPost]
        public IActionResult SaveEditPatient(Patient editedPatient)
        {


            var existingPatient = DbContext.patients.FirstOrDefault(ph => ph.Id == editedPatient.Id);


            if (existingPatient != null)
                {
                    existingPatient.name = editedPatient.name;
                    existingPatient.contact = editedPatient.contact;
                    existingPatient.birth_date = editedPatient.birth_date;
                    existingPatient.address = editedPatient.address;

                

                    DbContext.SaveChanges();
                }

                return RedirectToAction("index");
            

        }


        //delete btn
        [HttpPost]
        public IActionResult DeletePatient(int id)
        {
            var patientToDelete = DbContext.patients.Find(id);

            if (patientToDelete == null)
            {
                return NotFound();
            }

            DbContext.patients.Remove(patientToDelete);
            DbContext.SaveChanges();

            return RedirectToAction("Index"); 
        }

    }
}
