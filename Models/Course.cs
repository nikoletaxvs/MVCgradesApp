using System;
using System.Collections.Generic;

namespace GradesApp.Models;

public partial class Course
{
    public int IdCourse { get; set; }

    public string? CourseTitle { get; set; }

    public string? CourseSemester { get; set; }

    public int? ProfessorsAfm { get; set; }

    public virtual ICollection<CourseHasStudent> CourseHasStudents { get; } = new List<CourseHasStudent>();

    public virtual Professor? ProfessorsAfmNavigation { get; set; }
}
