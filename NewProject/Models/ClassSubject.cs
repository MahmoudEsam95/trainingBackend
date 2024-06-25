using System;
using System.Collections.Generic;

//namespace NewProject.Models;
namespace NewProject.Controllers;

public partial class ClassSubject
{
    public int Id { get; set; }

    public int ClassId { get; set; }

    public int SubjectId { get; set; }

    public int TeacherID { get; set; }


    public virtual Class Class { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;

}


public partial class ClassSubjectDto
{
    public int Id { get; set; }

    public int ClassId { get; set; }

    public int SubjectId { get; set; }

    public int TeacherID { get; set; }


}