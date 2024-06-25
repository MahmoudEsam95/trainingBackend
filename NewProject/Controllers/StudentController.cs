using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
//using NewProject.Models;


namespace NewProject.Controllers
{


    [ApiController]
    [Route("[controller]/[action]")]
    public class StudentController : ControllerBase
    {
        private readonly EduacationContext _context;

        private readonly ILogger<StudentController> _logger;


        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
            _context = new EduacationContext();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentDto studentDto)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    Id = studentDto.Id,
                    Name = studentDto.Name,
                    Address = studentDto.Address,
                    StageId = studentDto.StageId,
                    GradeId = studentDto.GradeId,
                    ClassesId = studentDto.ClassesId
                };

                _context.Add(student);
                await _context.SaveChangesAsync();
                await NotifyWebhook(student);
                return CreatedAtAction(nameof(GetByID), new { id = student.Id }, student);
            }
            return BadRequest(ModelState);
        }

        private async Task NotifyWebhook(Student student)
        {
            var webhookUrl = "http://localhost:5254/api/webhook";   //44384  //5263  //5254  //7074
            var jsonContent = JsonSerializer.Serialize(student);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.PostAsync(webhookUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Webhook notified successfully");
                }
                else
                {
                    Console.WriteLine("Failed to notify webhook");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred while notifying webhook: {ex.Message}");
            }
        }


        [HttpGet]
        public async Task<Student?> GetByID(int? id)
        {
            if (id == null)
            {
                return null;
            }


            var Student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            if (Student == null)
            {
                return null;
            }


            return Student;
        }


        [HttpGet]
        public async Task<IActionResult> GetLastId()
        {
            try
            {
                var lastId = await _context.Students.OrderByDescending(e => e.Id).Select(e => e.Id).FirstOrDefaultAsync();
                return Ok(lastId);
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal server error");
            }
        }



        [HttpGet]
        public Task<List<Student?>> GetAll()
        {

            var list = _context.Students.Include(m=>m.Grade).Include(m => m.Classes).Include(m => m.Stage).ToListAsync();
            if (list == null)
            {
                return null;
            }
            return list;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _context.Students.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Students.Remove(entity);
            _context.SaveChanges();

            return NoContent();
        }
    


        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] StudentDto studentDto)
        {
            if (id != studentDto.Id)
            {
                return BadRequest("Invalid ID");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingStudent = await _context.Students.FindAsync(id);
            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.Name = studentDto.Name;
            existingStudent.Address = studentDto.Address;
            existingStudent.StageId = studentDto.StageId;
            existingStudent.GradeId = studentDto.GradeId;
            existingStudent.ClassesId = studentDto.ClassesId;

            try
            {
                _context.Update(existingStudent);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(existingStudent);
        }








        private bool UserExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
