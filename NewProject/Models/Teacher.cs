using System;
using System.Collections.Generic;

//namespace NewProject.Models;
namespace NewProject.Controllers;

public partial class Teacher
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Mobile { get; set; }

    public string? Address { get; set; }

    //public virtual ICollection<TeacherWork> TeacherWorks { get; set; } = new List<TeacherWork>();
}
