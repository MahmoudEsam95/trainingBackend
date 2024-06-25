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
    public class StudentCountByClassController : ControllerBase
    {
        private readonly EduacationContext _context;
        private readonly ILogger<StudentCountByClassController> _logger;

        public StudentCountByClassController(ILogger<StudentCountByClassController> logger)
        {
            _logger = logger;
            _context = new EduacationContext();

        }
        [HttpGet]
        public IActionResult GetChartData()
        {
            var StudentCountByClasses = _context.StudentCountByClasses.ToList();

            if (StudentCountByClasses == null || StudentCountByClasses.Count == 0)
            {
                return NotFound(); // Return NotFound if there's no data
            }

            // Convert data to a format suitable for the doughnut chart
            var chartData = new
            {
                labels = StudentCountByClasses.Select(v => v.ClassName),
                values = StudentCountByClasses.Select(v => v.StudentCount)
            };

            return Ok(chartData); // Return the chart data

        }


        //[HttpGet]
        //public IActionResult GetChartData1()
        //{
        //    var StudentCountByClasses = _context.StudentCountByClasses.ToList();

        //    if (StudentCountByClasses == null || StudentCountByClasses.Count == 0)
        //    {
        //        return NotFound(); // Return NotFound if there's no data
        //    }


        //    int numberOfStudent = StudentCountByClasses.Count;

        //    // Prepare chart data
        //    var chartData1 = new
        //    {
        //        numberOfStudent
        //    };

        //    return Ok(chartData1); // Return the chart data
        //}
    }
}