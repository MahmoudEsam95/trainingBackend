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
    public class DegreeController : ControllerBase
    {
        private readonly EduacationContext _context;

        private readonly ILogger<DegreeController> _logger;




        public DegreeController(ILogger<DegreeController> logger)
        {
            _logger = logger;
            _context = new EduacationContext();
        }

        [HttpGet]
        public async Task<Degree?> GetByID(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var degree = await _context.Degrees.FirstOrDefaultAsync(m => m.Id == id);
            if (degree == null)
            {
                return null;
            }
            return degree;
        }
        [HttpGet("{studentId}")]
        public async Task<ActionResult<IEnumerable<Degree>>> GetDegreesByStudentId(int? studentId)
        {
            var degrees = await _context.Degrees.Include(e => e.Student).Include(e => e.Subject)
                .Where(d => d.StudentId == studentId)
                .ToListAsync();
            if (degrees == null || !degrees.Any())
            {
                return NotFound();
            }

            return degrees;
        }
        [HttpGet]
        public Task<List<Degree?>> GetAll()
        {
            var list = _context.Degrees.Include(e => e.Student).Include(e => e.Subject).ToListAsync();
            if (list == null)
            {
                return null;
            }
            return list;
        }




        [HttpGet]
        public async Task<IActionResult> GetLastId()
        {
            try
            {
                var lastId = await _context.Degrees.OrderByDescending(e => e.Id).Select(e => e.Id).FirstOrDefaultAsync();
                return Ok(lastId);
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal server error");
            }
        }



    
      

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Degrees.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Degrees.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        [HttpPost]
        public async Task<IActionResult> Create(DegreeDto degreeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var degree = new Degree
                {
                    Id = degreeDto.Id,
                    SubjectId = degreeDto.SubjectId,
                    StudentId = degreeDto.StudentId,
                    Degree1 = degreeDto.Degree1
                };

                _context.Add(degree);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetByID), new { id = degree.Id }, degree);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, DegreeDto DegreeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != DegreeDto.Id)
            {
                return BadRequest();
            }

            var entity = await _context.Degrees.FirstOrDefaultAsync(t => t.Id == id);

            if (entity == null)
            {
                return NotFound();
            }

            entity.Id = DegreeDto.Id;
            entity.SubjectId = DegreeDto.SubjectId;
            entity.StudentId = DegreeDto.StudentId;
            entity.Degree1 = DegreeDto.Degree1;


            try
            {
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

            return Ok(entity);
        }







        private bool UserExists(int id)
        {
            return _context.Degrees.Any(e => e.Id == id);
        }
    }
}
