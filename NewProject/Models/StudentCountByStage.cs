using System;
using System.Collections.Generic;

//namespace NewProject.Models;
namespace NewProject.Controllers;

public partial class StudentCountByStage
{
    public int? StageId { get; set; }

    public string? StageName { get; set; }

    public int? StudentCount { get; set; }
}
