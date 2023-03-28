using System;
using System.Collections.Generic;

namespace GradesApp.Models;

public partial class Secretary
{
    public int Phonenumber { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Department { get; set; } = null!;

    public string? UsersUsername { get; set; }

    public virtual User? UsersUsernameNavigation { get; set; }
}
