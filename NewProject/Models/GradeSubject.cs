using System;
using System.Collections.Generic;

//namespace NewProject.Models;
namespace NewProject.Controllers;

public partial class GradeSubject
{
    public int Id { get; set; }

    public int SubjectId { get; set; }

    public int GradeId { get; set; }

    public virtual Grade Grade { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}

public partial class GradeSubjectDto
{
    public int Id { get; set; }

    public int SubjectId { get; set; }

    public int GradeId { get; set; }

}
