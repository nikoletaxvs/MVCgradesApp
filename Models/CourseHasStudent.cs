using System;
using System.Collections.Generic;

namespace GradesApp.Models;

public partial class CourseHasStudent
{
    public int IdCourseHasStudents { get; set; }

    public int? CourseIdCourse { get; set; }

    public int? StudentsRegistrationNumber { get; set; }

    public int? GradeCourseStudent { get; set; }

    public virtual Course? CourseIdCourseNavigation { get; set; }

    public virtual Student? StudentsRegistrationNumberNavigation { get; set; }
}
