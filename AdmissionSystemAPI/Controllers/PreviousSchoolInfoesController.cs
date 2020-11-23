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
    public class PreviousSchoolInfoesController : ControllerBase
    {
        private readonly DbAdmissionSystem _context;

        public PreviousSchoolInfoesController(DbAdmissionSystem context)
        {
            _context = context;
        }

        // GET: api/PreviousSchoolInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PreviousSchoolInfo>>> GetPreviousSchoolInfos()
        {
            return await _context.PreviousSchoolInfos.ToListAsync();
        }

        // GET: api/PreviousSchoolInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PreviousSchoolInfo>> GetPreviousSchoolInfo(int id)
        {
            var previousSchoolInfo = await _context.PreviousSchoolInfos.FindAsync(id);

            if (previousSchoolInfo == null)
            {
                return NotFound();
            }

            return previousSchoolInfo;
        }

        // PUT: api/PreviousSchoolInfoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPreviousSchoolInfo(int id, PreviousSchoolInfo previousSchoolInfo)
        {
            if (id != previousSchoolInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(previousSchoolInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreviousSchoolInfoExists(id))
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

        // POST: api/PreviousSchoolInfoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PreviousSchoolInfo>> PostPreviousSchoolInfo(PreviousSchoolInfo previousSchoolInfo)
        {
            _context.PreviousSchoolInfos.Add(previousSchoolInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPreviousSchoolInfo", new { id = previousSchoolInfo.Id }, previousSchoolInfo);
        }

        // DELETE: api/PreviousSchoolInfoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PreviousSchoolInfo>> DeletePreviousSchoolInfo(int id)
        {
            var previousSchoolInfo = await _context.PreviousSchoolInfos.FindAsync(id);
            if (previousSchoolInfo == null)
            {
                return NotFound();
            }

            _context.PreviousSchoolInfos.Remove(previousSchoolInfo);
            await _context.SaveChangesAsync();

            return previousSchoolInfo;
        }

        private bool PreviousSchoolInfoExists(int id)
        {
            return _context.PreviousSchoolInfos.Any(e => e.Id == id);
        }
    }
}
