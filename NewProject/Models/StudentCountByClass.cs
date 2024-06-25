using System;
using System.Collections.Generic;

//namespace NewProject.Models;
namespace NewProject.Controllers;

public partial class StudentCountByClass
{
    public int? ClassesId { get; set; }

    public string? ClassName { get; set; }

    public int? StudentCount { get; set; }
}
