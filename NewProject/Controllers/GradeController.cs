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
    public class GradeController : ControllerBase
    {
        private readonly EduacationContext _context;

        private readonly ILogger<GradeController> _logger;




        public GradeController(ILogger<GradeController> logger)
        {
            _logger = logger;
            _context = new EduacationContext();
        }





        [HttpGet("GetByStageId/{stageId}")]
        public async Task<ActionResult<IEnumerable<Grade>>> GetByStageId(int stageId)
        {
            var grades = await _context.Grades
                .Where(g => g.StageId == stageId)
                .ToListAsync();

            if (grades == null)
            {
                return NotFound();
            }

            return grades;
        }



        [HttpGet]
        public async Task<Grade?> GetByID(int? id)
        {
            if (id == null)
            {
                return null;
            }


            var Class = await _context.Grades.Include(m=>m.Stage).FirstOrDefaultAsync(m => m.Id == id);
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
                var lastId = await _context.Grades.OrderByDescending(e => e.Id).Select(e => e.Id).FirstOrDefaultAsync();
                return Ok(lastId);
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal server error");
            }
        }



        [HttpGet]
        public Task<List<Grade?>> GetAll()
        {

            var list = _context.Grades.Include(m=>m.Stage).ToListAsync();
            if (list == null)
            {
                return null;
            }
            return list;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _context.Grades.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Grades.Remove(entity);
            _context.SaveChanges();

            return NoContent();
        }






        [HttpPost]

        public async Task<IActionResult> Create(GradeDto GradeDto)
        {
            if (ModelState.IsValid)
            {
                var Grade = new Grade
                {
                    Id = GradeDto.Id,
                    Name = GradeDto.Name,
                    StageId = GradeDto.StageId,


                };

                _context.Add(Grade);
                await _context.SaveChangesAsync();
                return Ok(Grade);
            }
            return BadRequest(ModelState);
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] GradeDto GradeDto)
        {
            if (id != GradeDto.Id)
            {
                return BadRequest("Invalid ID");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingGrade = await _context.Grades.FindAsync(id);
            if (existingGrade == null)
            {
                return NotFound();
            }
            existingGrade.Id = GradeDto.Id;
            existingGrade.Name = GradeDto.Name;
            existingGrade.StageId = GradeDto.StageId;


            try
            {
                _context.Update(existingGrade);
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

            return Ok(existingGrade);
        }











        private bool UserExists(int id)
        {
            return _context.Grades.Any(e => e.Id == id);
        }
    }
}
