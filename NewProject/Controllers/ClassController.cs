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
    public class ClassController : ControllerBase
    {
        private readonly EduacationContext _context;

        private readonly ILogger<ClassController> _logger;




        public ClassController(ILogger<ClassController> logger)
        {
            _logger = logger;
            _context = new EduacationContext();
        }





        [HttpGet("GetByGradeId/{gradeId}")]
        public async Task<ActionResult<IEnumerable<Class>>> GetByGradeId(int gradeId)
        {
            var classes = await _context.Classes
                .Where(c => c.GradeId == gradeId)
                .ToListAsync();

            if (classes == null)
            {
                return NotFound();
            }

            return classes;
        }



        [HttpGet]
        public async Task<Class?> GetByID(int? id)  
        {
            if (id == null)
            {
                return null;
            }


            var Class = await _context.Classes.Include(m => m.Grade).FirstOrDefaultAsync(m => m.Id == id);
            if (Class == null)
            {
                return null;
            }


            return Class;
        }


        [HttpGet]
        public async Task<IActionResult> GetLastId()
        {
            try
            {
                var lastId = await _context.Classes.OrderByDescending(e => e.Id).Select(e => e.Id).FirstOrDefaultAsync();
                return Ok(lastId);
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal server error");    
            }
        }



        [HttpGet]
        public Task<List<Class?>> GetAll()
        {

            var list = _context.Classes.Include(m=>m.Grade).ToListAsync();
            if (list == null)
            {
                return null;
            }
            return list;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _context.Classes.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Classes.Remove(entity);
            _context.SaveChanges();

            return NoContent();
        }


        [HttpPost]

        public async Task<IActionResult> Create(ClassDto ClassDto)
        {
            if (ModelState.IsValid)
            {
                var Class = new Class
                {
                    Id = ClassDto.Id,
                    Name = ClassDto.Name,
                    GradeId = ClassDto.GradeId,


                };

                _context.Add(Class);
                await _context.SaveChangesAsync();
                return Ok(Class);
            }
            return BadRequest(ModelState);
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ClassDto ClassDto)
        {
            if (id != ClassDto.Id)
            {
                return BadRequest("Invalid ID");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingClass = await _context.Classes.FindAsync(id);
            if (existingClass == null)
            {
                return NotFound();
            }
            existingClass.Id = ClassDto.Id;
            existingClass.Name = ClassDto.Name;
            existingClass.GradeId = ClassDto.GradeId;


            try
            {
                _context.Update(existingClass);
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

            return Ok(existingClass);
        }









        private bool UserExists(int id)
        {
            return _context.Classes.Any(e => e.Id == id);
        }
    }
}
