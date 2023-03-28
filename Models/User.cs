using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;

namespace GradesApp.Models;

public partial class User
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Role { get; set; }
    public virtual ICollection<Professor> Professors { get; } = new List<Professor>();

    public virtual ICollection<Secretary> Secretaries { get; } = new List<Secretary>();

    public virtual ICollection<Student> Students { get; } = new List<Student>();
}
