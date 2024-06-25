using System;
using System.Collections.Generic;

//namespace NewProject.Models;
namespace NewProject.Controllers;


public partial class Student
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public int? StageId { get; set; }

    public int? GradeId { get; set; }

    public int? ClassesId { get; set; }

    public virtual Class? Classes { get; set; }


    public virtual Grade? Grade { get; set; }

    public virtual Stage? Stage { get; set; }
}


public partial class StudentDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public int? StageId { get; set; }

    public int? GradeId { get; set; }

    public int? ClassesId { get; set; }

}
