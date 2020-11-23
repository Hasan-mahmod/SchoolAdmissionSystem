using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdmissionSystemAPI.Models;

namespace AdmissionSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionClassesController : ControllerBase
    {
        private readonly DbAdmissionSystem _context;

        public AdmissionClassesController(DbAdmissionSystem context)
        {
            _context = context;
        }

        // GET: api/AdmissionClasses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdmissionClass>>> GetAdmissionClasses()
        {
            return await _context.AdmissionClasses.ToListAsync();
        }

        // GET: api/AdmissionClasses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdmissionClass>> GetAdmissionClass(int id)
        {
            var admissionClass = await _context.AdmissionClasses.FindAsync(id);

            if (admissionClass == null)
            {
                return NotFound();
            }

            return admissionClass;
        }

        // PUT: api/AdmissionClasses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmissionClass(int id, AdmissionClass admissionClass)
        {
            if (id != admissionClass.Id)
            {
                return BadRequest();
            }

            _context.Entry(admissionClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdmissionClassExists(id))
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

        // POST: api/AdmissionClasses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AdmissionClass>> PostAdmissionClass(AdmissionClass admissionClass)
        {
            _context.AdmissionClasses.Add(admissionClass);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdmissionClass", new { id = admissionClass.Id }, admissionClass);
        }

        // DELETE: api/AdmissionClasses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AdmissionClass>> DeleteAdmissionClass(int id)
        {
            var admissionClass = await _context.AdmissionClasses.FindAsync(id);
            if (admissionClass == null)
            {
                return NotFound();
            }

            _context.AdmissionClasses.Remove(admissionClass);
            await _context.SaveChangesAsync();

            return admissionClass;
        }

        private bool AdmissionClassExists(int id)
        {
            return _context.AdmissionClasses.Any(e => e.Id == id);
        }
    }
}
