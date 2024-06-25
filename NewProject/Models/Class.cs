using System;
using System.Collections.Generic;

//namespace NewProject.Models;
namespace NewProject.Controllers;

public partial class Class
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? GradeId { get; set; }

    public virtual Grade? Grade { get; set; }

    //public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    //public virtual ICollection<TeacherWork> TeacherWorks { get; set; } = new List<TeacherWork>();
}

public partial class ClassDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? GradeId { get; set; }




}
    