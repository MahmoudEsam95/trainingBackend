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
    public class TeacherWorkController : ControllerBase
    {
        private readonly EduacationContext _context;

        private readonly ILogger<TeacherWorkController> _logger;




        public TeacherWorkController(ILogger<TeacherWorkController> logger)
        {
            _logger = logger;
            _context = new EduacationContext();
        }






        [HttpGet("{teacherId}")]
        public async Task<ActionResult<IEnumerable<TeacherWork>>> GetTeacherWorkByTeacherId(int teacherId)
        {
            var teacherWorks = await _context.TeacherWorks
                .Include(tw => tw.Class)
                .Include(tw => tw.Subject)
                .Include(tw => tw.Teacher)
                .Where(tw => tw.TeacherId == teacherId)
                .ToListAsync();

            if (teacherWorks == null || !teacherWorks.Any())
            {
                return null;
            }

            return teacherWorks;
        }



        [HttpGet]
        public async Task<TeacherWork?> GetByID(int? id)
        {
            if (id == null)
            {
                return null;
            }


            var TeacherWork = await _context.TeacherWorks.FirstOrDefaultAsync(m => m.Id == id);
            if (TeacherWork == null)
            {
                return null;
            }


            return TeacherWork;
        }


        [HttpGet]
        public async Task<IActionResult> GetLastId()
        {
            try
            {
                var lastId = await _context.TeacherWorks.OrderByDescending(e => e.Id).Select(e => e.Id).FirstOrDefaultAsync();
                return Ok(lastId);
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal server error");
            }
        }



        [HttpGet]
        public Task<List<TeacherWork?>> GetAll()
        {

            var list = _context.TeacherWorks.ToListAsync();
            if (list == null)
            {
                return null;
            }
            return list;
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacherWork(int id)
        {
            var teacherWork = await _context.TeacherWorks.FindAsync(id);
            if (teacherWork == null)
            {
                return NotFound();
            }

            _context.TeacherWorks.Remove(teacherWork);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPost]

        public async Task<IActionResult> Create(TeacherWorkDto TeacherWorkDto)
        {
            if (ModelState.IsValid)
            {
                var TeacherWork = new TeacherWork
                {
                    Id = TeacherWorkDto.Id,
                    TeacherId = TeacherWorkDto.TeacherId,
                    SubjectId = TeacherWorkDto.SubjectId,
                    ClassId = TeacherWorkDto.ClassId

                };

                _context.Add(TeacherWork);
                await _context.SaveChangesAsync();
                return Ok(TeacherWork);
            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, TeacherWorkDto TeacherWorkDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != TeacherWorkDto.Id)
            {
                return BadRequest();
            }

            var entity = await _context.TeacherWorks.FirstOrDefaultAsync(t => t.Id == id);

            if (entity == null)
            {
                return NotFound();
            }

            entity.TeacherId = TeacherWorkDto.TeacherId;
            entity.SubjectId = TeacherWorkDto.SubjectId;
            entity.ClassId = TeacherWorkDto.ClassId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherWorkExists(id))
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

        private bool TeacherWorkExists(int id)
        {
            return _context.TeacherWorks.Any(e => e.Id == id);
        }
    



    private bool UserExists(int id)
        {
            return _context.TeacherWorks.Any(e => e.Id == id);
        }
    }
}
