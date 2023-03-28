using GradesApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace GradesApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly GradesAppContext _db;
        public HomeController(GradesAppContext db)
        {
            _db = db;
        }
        public IActionResult LogIn()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogIn(User obj)
        {
            if(!_db.Users.Any(o=>o.Username == obj.Username))
            {
                ModelState.AddModelError("Username","This username does not exist");
            }
         
            if(ModelState.IsValid) {
                //check that this user exists in the database
                var checkUserName = _db.Users.Where(x => x.Username == obj.Username).FirstOrDefault();
                if (checkUserName!=null)
                {
                    //check that the given password is correct
                    if (checkUserName.Password == obj.Password)
                    {
                    
                            
                            TempData["users_name"] = obj.Username;
                            return RedirectToAction("Index");
                        }
                    else {
                        ModelState.AddModelError("password", "Λάθος κωδικός");
                    }
                
                }
            }
            return View(obj);
        }
        public IActionResult SignUp() {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(User obj)
        {
            if ((Regex.Replace(obj.Username,"/s","")).Length <4)
            {
                ModelState.AddModelError("Username","Username must contain at least 4 characters");
            }
            if(_db.Users.Any(o=> o.Username == obj.Username))
            {
                ModelState.AddModelError("Username", "This username is taken");
            }
            if(obj.Password==null || obj.Password.Length==0)
            {
                ModelState.AddModelError("Password", "Password is invalid");
            }
            if (ModelState.IsValid) {
               
                _db.Users.Add(obj);
                _db.SaveChanges();
                TempData["Username"] = obj.Username;
                TempData["password"] = obj.Password;
                TempData["Role"] = obj.Role;
                TempData["AlertMessage"] = "Ο χρήστης "+ obj.Username +" καταχωρήθηκε στο σύστημα με επιτυχία!";
                return RedirectToAction("LogIn");
            }
          
            return View(obj);
        }
            public IActionResult Index()
        {
            //ViewBag.Message = TempData["users_name"].ToString();
            ViewBag.Message = "Unknown";
            return View();
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}