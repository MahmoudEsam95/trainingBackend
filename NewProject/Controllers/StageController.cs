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
    public class StageController : ControllerBase
    {
        private readonly EduacationContext _context;

        private readonly ILogger<StageController> _logger;




        public StageController(ILogger<StageController> logger)
        {
            _logger = logger;
            _context = new EduacationContext();
        }





        [HttpGet]
        public async Task<IActionResult> GetLastId()
        {
            try
            {
                var lastId = await _context.Stages.OrderByDescending(e => e.Id).Select(e => e.Id).FirstOrDefaultAsync();
                return Ok(lastId);
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal server error");
            }
        }




        [HttpGet]
        public async Task<Stage?> GetByID(int? id)
        {
            if (id == null)
            {
                return null;
            }


            var Class = await _context.Stages/*.Include(m=>m.Grade)*/.FirstOrDefaultAsync(m => m.Id == id);
            if (Class == null)
            {
                return null;
            }


            return Class;
        }




        [HttpGet]
        public Task<List<Stage?>> GetAll()
        {

            var list = _context.Stages.ToListAsync();
            if (list == null)
            {
                return null;
            }
            return list;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _context.Stages.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Stages.Remove(entity);
            _context.SaveChanges();

            return NoContent();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Grade,GradeId")] Stage Stage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Stage);
                await _context.SaveChangesAsync();
                return Ok(Stage);
            }
            return Ok(Stage);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Grade,GradeIdE")] Stage Stage)
        {
            if (id != Stage.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Stage.Id = id;
                _context.Update(Stage);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(Stage.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return Ok(Stage);
        }











        private bool UserExists(int id)
        {
            return _context.Stages.Any(e => e.Id == id);
        }
    }
}
