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
    public class SubjectController : ControllerBase
    {
        private readonly EduacationContext _context;

        private readonly ILogger<SubjectController> _logger;




        public SubjectController(ILogger<SubjectController> logger)
        {
            _logger = logger;
            _context = new EduacationContext();
        }





        [HttpGet]
        public async Task<IActionResult> GetLastId()
        {
            try
            {
                var lastId = await _context.Subjects.OrderByDescending(e => e.Id).Select(e => e.Id).FirstOrDefaultAsync();
                return Ok(lastId);
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal server error");
            }
        }




        [HttpGet]
        public async Task<Subject?> GetByID(int? id)
        {
            if (id == null)
            {
                return null;
            }


            var Subject = await _context.Subjects/*.Include(m=>m.Grade)*/.FirstOrDefaultAsync(m => m.Id == id);
            if (Subject == null)
            {
                return null;
            }


            return Subject;
        }




        [HttpGet]
        public Task<List<Subject?>> GetAll()
        {

            var list = _context.Subjects.ToListAsync();
            if (list == null)
            {
                return null;
            }
            return list;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _context.Subjects.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Subjects.Remove(entity);
            _context.SaveChanges();

            return NoContent();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name")] Subject Subject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Subject);
                await _context.SaveChangesAsync();
                return Ok(Subject);
            }
            return Ok(Subject);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Subject Subject)
        {
            if (id != Subject.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Subject.Id = id;
                _context.Update(Subject);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(Subject.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return Ok(Subject);
        }











        private bool UserExists(int id)
        {
            return _context.Subjects.Any(e => e.Id == id);
        }
    }
}
