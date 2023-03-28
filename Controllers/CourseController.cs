using GradesApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Linq;

namespace GradesApp.Controllers
{
    public class CourseController : Controller
    {

        private readonly GradesAppContext _db;
        public CourseController(GradesAppContext db)
        {
            _db = db;
        }
        //GET
        public IActionResult ShowGradedCourses()
        {
            ViewData["Professors"]=_db.Professors.ToList();
            ViewData["Courses"]= _db.Courses.ToList();
            ViewData["CourseHasStudents"] =_db.CourseHasStudents.ToList();
         
            
            return View();
        }
        //GET
        public IActionResult ShowGradedCoursesStudent()
        {
            ViewData["Students"] = _db.Students.ToList();
            ViewData["Courses"] = _db.Courses.ToList();
            ViewData["CourseHasStudents"] = _db.CourseHasStudents.ToList();


            return View();
        }
        
        //GET
        public IActionResult ShowGradedPerSemester()
        {
            ViewData["Students"] = _db.Students.ToList();
            ViewData["Courses"] = _db.Courses.ToList();
            ViewData["CourseHasStudents"] = _db.CourseHasStudents.ToList();

            return View();
        } 
        //GET
        public IActionResult GradesPerStudent()
        {
            ViewData["Students"] = _db.Students.ToList();
            ViewData["Courses"] = _db.Courses.ToList();
            ViewData["CourseHasStudents"] = _db.CourseHasStudents.ToList();

            return View();
        }


        //GET
        public IActionResult GradeStudent()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GradeStudent(CourseHasStudent obj)
        {

            if (ModelState.IsValid)
            {
                /*
                 * If the student's registration number coexists with the course's courseID 
                 * and the grade field is null
                 * -> We consider the course not graded
                 */
                if (_db.CourseHasStudents.Any(o => o.StudentsRegistrationNumber == obj.StudentsRegistrationNumber && o.CourseIdCourse == obj.CourseIdCourse && o.GradeCourseStudent == null))
                {
                    var user = _db.CourseHasStudents.FirstOrDefault(o => o.StudentsRegistrationNumber == obj.StudentsRegistrationNumber && o.CourseIdCourse == obj.CourseIdCourse && o.GradeCourseStudent == null);
                    user.GradeCourseStudent = obj.GradeCourseStudent;
                    _db.SaveChanges();
                    TempData["AlertMessage"] = "Ο βαθμός καταχωρήθηκε με επιτυχία!";

                    return RedirectToAction("Index", "Home");
                }
                if (_db.CourseHasStudents.Any(o => o.StudentsRegistrationNumber == obj.StudentsRegistrationNumber && o.CourseIdCourse == obj.CourseIdCourse)) {
                    ModelState.AddModelError("StudentsRegistrationNumber", "This student has been graded");
                    return View(obj);
                }
                if (!(_db.Students.Any(o => o.RegistrationNumber == obj.StudentsRegistrationNumber)))
                {
                    ModelState.AddModelError("StudentsRegistrationNumber", "Student is not registred in the database");
                    return View(obj);
                }
                if (!(_db.Courses.Any(o => o.IdCourse == obj.CourseIdCourse)))
                {
                    ModelState.AddModelError("CourseIdCourse", "Course is not registred in the database");
                    return View(obj);
                }
                if (!_db.CourseHasStudents.Any(o => o.StudentsRegistrationNumber == obj.StudentsRegistrationNumber && o.CourseIdCourse==obj.CourseIdCourse))
                {
                    ModelState.AddModelError("StudentsRegistrationNumber", "Student is not registred in this lesson");
                    return View(obj);
                }
                
                
            }
            return View(obj);
        }
        //get
        public IActionResult AssignToProfessor(int? id)
        {
            if(id==null || id < 0){
                return NotFound();
            }
            var courseFromId = _db.Courses.Find(id);
            if(courseFromId == null)
            {
                return NotFound();
            }
            ViewData["Professors"] = GetProfessors();
            ViewData["Courses"] = GetCourses();
            

            return View(courseFromId);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignToProfessor(Course obj)
        {

            if (ModelState.IsValid)
            {

                _db.Courses.Update(obj);
                _db.SaveChanges();
                TempData["AlertMessage"] = "Το μάθημα " + obj.CourseTitle + " ανατέθηκε στον καθηγητή " + _db.Professors.Find(obj.ProfessorsAfm).Name +" "+ _db.Professors.Find(obj.ProfessorsAfm).Surname + "!";
                return RedirectToAction("Index", "Home");
            }
            return View(obj);
        }
        //GET
        public IActionResult AssignToStudent(int id)
        {
            if (id == null || id < 0)
            {
                return NotFound();
            }
          
            ViewData["Students"] = GetStudents();
            ViewData["Course"] = id;
          
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignToStudent(CourseHasStudent obj)
        {

            if (ModelState.IsValid)
            {
                if(_db.CourseHasStudents.Any(o => o.StudentsRegistrationNumber == obj.StudentsRegistrationNumber && o.CourseIdCourse == obj.CourseIdCourse))
                {
                    ModelState.AddModelError("StudentsRegistrationNumber", "Studnet is already registred");
                    return View(obj);
                }
                obj.IdCourseHasStudents = _db.CourseHasStudents.Count() + 1;//autoincrement
                    _db.CourseHasStudents.Add(obj);
                    _db.SaveChanges();
                  //  TempData["AlertMessage"] = "Ο μαθητής " + _db.Students.Find(obj.StudentsRegistrationNumber).Name + " " + _db.Students.Find(obj.StudentsRegistrationNumber).Surname + " εγγράφθηκε στο μάθημα " + _db.Courses.Find(obj.CourseIdCourse).CourseTitle + "!";

                    return RedirectToAction("Index", "Home");
                
              
            }
            return View(obj);
        }
        //GET
        public IActionResult AllCourses()
        {
            IEnumerable<Course> objCourseList = _db.Courses;
            return View(objCourseList);
        }

        private List<Professor> GetProfessors() {

            List<Professor> professors = _db.Professors.ToList();
            return professors;
        }
        private List<Student> GetStudents() {

            List<Student> students = _db.Students.ToList();
            return students;
        }
        private List<Course> GetCourses() {

            List<Course> courses = _db.Courses.ToList();
            return courses;
        }
       
    }
}
