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
    public class ApplyFormsController : ControllerBase
    {
        private readonly DbAdmissionSystem _context;

        public ApplyFormsController(DbAdmissionSystem context)
        {
            _context = context;
        }

        // GET: api/ApplyForms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplyForm>>> GetApplyForms()
        {
            return await _context.ApplyForms.ToListAsync();
        }

        // GET: api/ApplyForms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplyForm>> GetApplyForm(int id)
        {
            var applyForm = await _context.ApplyForms.FindAsync(id);

            if (applyForm == null)
            {
                return NotFound();
            }

            return applyForm;
        }

        // PUT: api/ApplyForms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplyForm(int id, ApplyForm applyForm)
        {
            if (id != applyForm.Id)
            {
                return BadRequest();
            }

            _context.Entry(applyForm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplyFormExists(id))
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

        // POST: api/ApplyForms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ApplyForm>> PostApplyForm(ApplyForm applyForm)
        {
            _context.ApplyForms.Add(applyForm);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplyForm", new { id = applyForm.Id }, applyForm);
        }

        // DELETE: api/ApplyForms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApplyForm>> DeleteApplyForm(int id)
        {
            var applyForm = await _context.ApplyForms.FindAsync(id);
            if (applyForm == null)
            {
                return NotFound();
            }

            _context.ApplyForms.Remove(applyForm);
            await _context.SaveChangesAsync();

            return applyForm;
        }

        private bool ApplyFormExists(int id)
        {
            return _context.ApplyForms.Any(e => e.Id == id);
        }
    }
}
