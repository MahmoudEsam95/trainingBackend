using System;
using System.Collections.Generic;

//namespace NewProject.Models;
namespace NewProject.Controllers;

public partial class Subject
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    //public virtual ICollection<Degree> Degrees { get; set; } = new List<Degree>();

    //public virtual ICollection<GradeSubject> GradeSubjects { get; set; } = new List<GradeSubject>();

    //public virtual ICollection<TeacherWork> TeacherWorks { get; set; } = new List<TeacherWork>();
}
