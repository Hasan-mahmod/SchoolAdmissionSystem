using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdmissionSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace AdmissionSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EducationSystemsController : ControllerBase
    {
        private readonly DbAdmissionSystem _context;

        public EducationSystemsController(DbAdmissionSystem context)
        {
            _context = context;
        }
        //[Authorize(Roles ="Admin")]
        // GET: api/EducationSystems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EducationSystem>>> GetEducationSystems()
        {
            return await _context.EducationSystems.ToListAsync();
        }

        // GET: api/EducationSystems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EducationSystem>> GetEducationSystem(int id)
        {
            var educationSystem = await _context.EducationSystems.FindAsync(id);

            if (educationSystem == null)
            {
                return NotFound();
            }

            return educationSystem;
        }

        // PUT: api/EducationSystems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEducationSystem(int id, EducationSystem educationSystem)
        {
            if (id != educationSystem.Id)
            {
                return BadRequest();
            }

            _context.Entry(educationSystem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EducationSystemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EducationSystems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EducationSystem>> PostEducationSystem(EducationSystem educationSystem)
        {
            _context.EducationSystems.Add(educationSystem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEducationSystem", new { id = educationSystem.Id }, educationSystem);
        }

        // DELETE: api/EducationSystems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EducationSystem>> DeleteEducationSystem(int id)
        {
            var educationSystem = await _context.EducationSystems.FindAsync(id);
            if (educationSystem == null)
            {
                return NotFound();
            }

            _context.EducationSystems.Remove(educationSystem);
            await _context.SaveChangesAsync();

            return educationSystem;
        }

        private bool EducationSystemExists(int id)
        {
            return _context.EducationSystems.Any(e => e.Id == id);
        }
    }
}
