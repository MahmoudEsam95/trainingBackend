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
    public class GradeSubjectController : ControllerBase
    {
        private readonly EduacationContext _context;

        private readonly ILogger<GradeSubjectController> _logger;




        public GradeSubjectController(ILogger<GradeSubjectController> logger)
        {
            _logger = logger;
            _context = new EduacationContext();
        }









        [HttpGet]
        public async Task<GradeSubject?> GetByID(int? id)
        {
            if (id == null)
            {
                return null;
            }


            var GradeSubject = await _context.GradeSubjects.FirstOrDefaultAsync(m => m.Id == id);
            if (GradeSubject == null)
            {
                return null;
            }


            return GradeSubject;
        }


        [HttpGet]
        public async Task<IActionResult> GetLastId()
        {
            try
            {
                var lastId = await _context.GradeSubjects.OrderByDescending(e => e.Id).Select(e => e.Id).FirstOrDefaultAsync();
                return Ok(lastId);
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal server error");
            }
        }



        [HttpGet]
        public Task<List<GradeSubject?>> GetAll()
        {

            var list = _context.GradeSubjects.Include(m=>m.Grade).Include(m => m.Subject).ToListAsync();
            if (list == null)
            {
                return null;
            }
            return list;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _context.GradeSubjects.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.GradeSubjects.Remove(entity);
            _context.SaveChanges();

            return NoContent();
        }


        [HttpPost]

        public async Task<IActionResult> Create(GradeSubjectDto GradeSubjectDto)
        {
            if (ModelState.IsValid)
            {
                var GradeSubject = new GradeSubject
                {
                    Id = GradeSubjectDto.Id,
                    SubjectId = GradeSubjectDto.SubjectId,
                    GradeId = GradeSubjectDto.GradeId,

                };

                _context.Add(GradeSubject);
                await _context.SaveChangesAsync();
                return Ok(GradeSubject);
            }
            return BadRequest(ModelState);
        }



        //[HttpPut("{id}")]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Grade,GradeId")] Student Student)
        //{
        //    if (id != Student.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        Student.Id = id;
        //        _context.Update(Student);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(Student.Id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }


        //    return Ok(Student);
        //}



        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] GradeSubjectDto GradeSubjectDto)
        {
            if (id != GradeSubjectDto.Id)
            {
                return BadRequest("Invalid ID");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingGradeSubject = await _context.GradeSubjects.FindAsync(id);
            if (existingGradeSubject == null)
            {
                return NotFound();
            }

            existingGradeSubject.Id = GradeSubjectDto.Id;
            existingGradeSubject.SubjectId = GradeSubjectDto.SubjectId;
            existingGradeSubject.GradeId= GradeSubjectDto.GradeId;
           

            try
            {
                _context.Update(existingGradeSubject);
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

            return Ok(existingGradeSubject);
        }








        private bool UserExists(int id)
        {
            return _context.GradeSubjects.Any(e => e.Id == id);
        }
    }
}
