using GradesApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GradesApp.Controllers
{
    public class UserController : Controller
    {
        private readonly GradesAppContext _db;
        public UserController(GradesAppContext db)
        {
            _db = db;
        }
       
        ////GET
        //public IActionResult CreateUser() {
        //    return View();
        //}
        ////POST
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult CreateUser(User obj)
        //{
        //    if (_db.Users.Any(o => o.Username ==obj.Username))
        //    {
        //        ModelState.AddModelError("username", "Sorry, this username is taken !");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        _db.Users.Add(obj);
        //        _db.SaveChanges();
        //        return RedirectToAction("Index", "Home");
        //    }
        //    return View(obj);
        //} 
        //GET
        public IActionResult CreateStudent() {

            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateStudent(Student obj)
        {
            if (_db.Students.Any(o => o.RegistrationNumber ==obj.RegistrationNumber))
            {
                ModelState.AddModelError("RegistrationNumber", "Sorry, this registration number is taken !");
            }
            if ((_db.Users.Any(o => o.Username == obj.UsersUsername)))
            {
                if (_db.Users.Find(obj.UsersUsername).Role != string.Empty)
                {
                    if (_db.Users.Find(obj.UsersUsername).Role.ToLower() != "student")
                    {
                        ModelState.AddModelError("RegistrationNumber", "Sorry, this registration number doesn't exist !");
                    }
                }
               
            }
            else {
                ModelState.AddModelError("UsersUsername", "Sorry, this username isn't available");
            }
            if (ModelState.IsValid)
            {
                _db.Students.Add(obj);
                _db.SaveChanges();
                TempData["AlertMessage"] = "Ο μαθητής καταχωρήθηκε με επιτυχία!";
                return RedirectToAction("Index", "Home");
            }
            return View(obj);
        }
        //GET
        public IActionResult CreateProfessor()
        {

            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProfessor(Professor obj)
        {
            if (_db.Professors.Any(o => o.Afm == obj.Afm))
            {
                ModelState.AddModelError("Afm", "Sorry, this afm number is taken !");
            }
            if (ModelState.IsValid)
            {
                _db.Professors.Add(obj);
                _db.SaveChanges();
                TempData["AlertMessage"] = "Ο καθηγητής καταχωρήθηκε με επιτυχία!";
                return RedirectToAction("Index", "Home");
            }
            return View(obj);
        }//GET
        public IActionResult CreateCourse()
        {

            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCourse(Course obj)
        {
            if (!_db.Professors.Any(o => o.Afm == obj.ProfessorsAfm))
            {
                ModelState.AddModelError("Afm", "Sorry, this professor afm doesn't exist!");
            }
            if (_db.Courses.Any(o => o.CourseTitle == obj.CourseTitle))
            {
                ModelState.AddModelError("Afm", "Sorry, this course title is already taken!");
            }
            if (ModelState.IsValid)
            {
                obj.IdCourse = _db.Courses.Count() + 1;
                _db.Courses.Add(obj);
                _db.SaveChanges();
                TempData["AlertMessage"] = "Το μάθημα καταχωρήθηκε με επιτυχία!";
                return RedirectToAction("Index", "Home");
            }
            return View(obj);
        }


    }



}
