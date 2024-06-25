using System;
using System.Collections.Generic;

//namespace NewProject.Models;
namespace NewProject.Controllers;

public partial class Grade
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? StageId { get; set; }

    //public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    //public virtual ICollection<GradeSubject> GradeSubjects { get; set; } = new List<GradeSubject>();

    public virtual Stage? Stage { get; set; }

    //public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}


public partial class GradeDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? StageId { get; set; }

}
