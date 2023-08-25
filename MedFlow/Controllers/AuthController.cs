using MedFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedFlow.Controllers
{

    class User
    {
        private string _username = "";
        private string _password = "";
        private int _user_type = 0;

        public string Username
        {
            get { return _username; }
            set { _username = value ?? ""; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value ?? ""; }
        }

        public int User_type
        {
            get { return _user_type; }
            set { _user_type = value; }
        }
    }

    public class AuthController : Controller
    {
        private User _authuser;
        private readonly ApplicationDbContext DbContext;
        public AuthController(ApplicationDbContext Context)
        {
            DbContext = Context;
        }


        public IActionResult Index()
        {
            return View("index");
        }

        [HttpPost]
        public IActionResult checkAuth(String username,String password)
        {
            if (username == null || password ==null)
            {
                return RedirectToAction("index");
            }
            var user = DbContext.userdetails.FirstOrDefault(u => u.username == username && u.password == password);
            
            if (user != null)
            {
                _authuser = new User
                {
                    Username = user.username,
                    Password = user.password,
                    User_type = user.user_type
                };
                // Successful login
                if(user.user_type == 1)
                {
                    return RedirectToAction("Adashboard", "Dashboard");
                }
                return RedirectToAction("Ddashboard", "Dashboard");
            }
            else
            {
                // Failed login
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View();
            }
            
        }
    }
}
