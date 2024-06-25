using System;
using System.Collections.Generic;

//namespace NewProject.Models;
namespace NewProject.Controllers;


public partial class Degree
{
    public int Id { get; set; }

    public int SubjectId { get; set; }

    public int StudentId { get; set; }

    public decimal? Degree1 { get; set; }

    public virtual Student Student { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}


public partial class DegreeDto
{
    public int Id { get; set; }

    public int SubjectId { get; set; }

    public int StudentId { get; set; }

    public decimal? Degree1 { get; set; }

 
}
