using System;
using System.Collections.Generic;
using System.Linq;
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
    public class TeacherController : ControllerBase
    {
        private readonly EduacationContext _context;

        private readonly ILogger<TeacherController> _logger;




        public TeacherController(ILogger<TeacherController> logger)
        {
            _logger = logger;
            _context = new EduacationContext();
        }





        [HttpGet]
        public async Task<IActionResult> GetLastId()
        {
            try
            {
                var lastId = await _context.Teachers.OrderByDescending(e => e.Id).Select(e => e.Id).FirstOrDefaultAsync();
                return Ok(lastId);
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal server error");
            }
        }




        [HttpGet]
        public async Task<Teacher?> GetByID(int? id)
        {
            if (id == null)
            {
                return null;
            }


            var Teacher = await _context.Teachers/*.Include(m=>m.Grade)*/.FirstOrDefaultAsync(m => m.Id == id);
            if (Teacher == null)
            {
                return null;
            }


            return Teacher;
        }




        [HttpGet]
        public Task<List<Teacher?>> GetAll()
        {

            var list = _context.Teachers.ToListAsync();
            if (list == null)
            {
                return null;
            }
            return list;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _context.Teachers.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(entity);
            _context.SaveChanges();

            return NoContent();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Mobile,Address")] Teacher Teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Teacher);
                await _context.SaveChangesAsync();
                return Ok(Teacher);
            }
            return Ok(Teacher);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Mobile,Address")] Teacher Teacher)
        {
            if (id != Teacher.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Teacher.Id = id;
                _context.Update(Teacher);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(Teacher.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return Ok(Teacher);
        }











        private bool UserExists(int id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        }
    }
}
