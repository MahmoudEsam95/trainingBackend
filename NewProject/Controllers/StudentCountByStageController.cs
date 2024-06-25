using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
//using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewProject.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class StudentCountByStageController : ControllerBase
    {
        private readonly EduacationContext _context;
        private readonly ILogger<StudentCountByStageController> _logger;

        public StudentCountByStageController(ILogger<StudentCountByStageController> logger)
        {
            _logger = logger;
            _context = new EduacationContext();

        }
        [HttpGet]
        public IActionResult GetChartData()
        {
            var StudentCountByStages = _context.StudentCountByStages.ToList();

            if (StudentCountByStages == null || StudentCountByStages.Count == 0)
            {
                return NotFound(); // Return NotFound if there's no data
            }

            // Convert data to a format suitable for the doughnut chart
            var chartData = new
            {
                labels = StudentCountByStages.Select(v => v.StageName),
                values = StudentCountByStages.Select(v => v.StudentCount)
            };

            return Ok(chartData); // Return the chart data

        }


        //[HttpGet]
        //public IActionResult GetChartData1()
        //{
        //    var StudentCountByStages = _context.StudentCountByStages.ToList();

        //    if (StudentCountByStages == null || StudentCountByStages.Count == 0)
        //    {
        //        return NotFound(); // Return NotFound if there's no data
        //    }


        //    int numberOfStudent = StudentCountByStages.Count;

        //    // Prepare chart data
        //    var chartData1 = new
        //    {
        //        numberOfStudent
        //    };

        //    return Ok(chartData1); // Return the chart data
        //}
    }
}