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
    public class ClassSubjectController : ControllerBase
    {
        private readonly EduacationContext _context;

        private readonly ILogger<ClassSubjectController> _logger;




        public ClassSubjectController(ILogger<ClassSubjectController> logger)
        {
            _logger = logger;
            _context = new EduacationContext();
        }









        [HttpGet]
        public async Task<ClassSubject?> GetByID(int? id)
        {
            if (id == null)
            {
                return null;
            }


            var ClassSubject = await _context.ClassSubjects.FirstOrDefaultAsync(m => m.Id == id);
            if (ClassSubject == null)
            {
                return null;
            }


            return ClassSubject;
        }


        [HttpGet]
        public async Task<IActionResult> GetLastId()
        {
            try
            {
                var lastId = await _context.ClassSubjects.OrderByDescending(e => e.Id).Select(e => e.Id).FirstOrDefaultAsync();
                return Ok(lastId);
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{teacherId}")]
        public async Task<ActionResult<IEnumerable<ClassSubject>>> GetTeacherWorkByTeacherId(int teacherId)
        {
            var ClassSubject = await _context.ClassSubjects
                .Include(tw => tw.Class)
                .Include(tw => tw.Subject)
                .Include(tw => tw.Teacher)
                .Where(tw => tw.TeacherID == teacherId)
                .ToListAsync();

            if (ClassSubject == null || !ClassSubject.Any())
            {
                return null;
            }

            return ClassSubject;
        }


        [HttpGet]
        public Task<List<ClassSubject?>> GetAll()
        {

            var list = _context.ClassSubjects.Include(m=>m.Teacher).Include(m => m.Subject).Include(m => m.Class).ToListAsync();
            if (list == null)
            {
                return null;
            }
            return list;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _context.ClassSubjects.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.ClassSubjects.Remove(entity);
            _context.SaveChanges();

            return NoContent();
        }


        [HttpPost]

        public async Task<IActionResult> Create(ClassSubjectDto ClassSubjectDto)
        {
            if (ModelState.IsValid)
            {
                var ClassSubject = new ClassSubject
                {
                    Id = ClassSubjectDto.Id,
                    SubjectId = ClassSubjectDto.SubjectId,
                    ClassId = ClassSubjectDto.ClassId,
                    TeacherID = ClassSubjectDto.TeacherID,


                };

                _context.Add(ClassSubject);
                await _context.SaveChangesAsync();
                return Ok(ClassSubject);
            }
            return BadRequest(ModelState);
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ClassSubjectDto ClassSubjectDto)
        {
            if (id != ClassSubjectDto.Id)
            {
                return BadRequest("Invalid ID");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingClassSubject = await _context.ClassSubjects.FindAsync(id);
            if (existingClassSubject == null)
            {
                return NotFound();
            }

            existingClassSubject.Id = ClassSubjectDto.Id;
            existingClassSubject.SubjectId = ClassSubjectDto.SubjectId;
            existingClassSubject.ClassId = ClassSubjectDto.ClassId;
            existingClassSubject.TeacherID = ClassSubjectDto.TeacherID;



            try
            {
                _context.Update(existingClassSubject);
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

            return Ok(existingClassSubject);
        }








        private bool UserExists(int id)
        {
            return _context.ClassSubjects.Any(e => e.Id == id);
        }
    }
}
