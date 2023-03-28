using System;
using System.Collections.Generic;

namespace GradesApp.Models;

public partial class Professor
{
    public int Afm { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Department { get; set; }

    public string? UsersUsername { get; set; }

    public virtual ICollection<Course> Courses { get; } = new List<Course>();

    public virtual User? UsersUsernameNavigation { get; set; }
}
