using System;
using System.Collections.Generic;

//namespace NewProject.Models;
namespace NewProject.Controllers;

public partial class Stage
{
    public int Id { get; set; }

    public string? Name { get; set; }

    //public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    //public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
