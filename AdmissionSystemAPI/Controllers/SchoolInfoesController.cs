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
    public class SchoolInfoesController : ControllerBase
    {
        private readonly DbAdmissionSystem _context;

        public SchoolInfoesController(DbAdmissionSystem context)
        {
            _context = context;
        }

        // GET: api/SchoolInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolInfo>>> GetSchoolInfos()
        {
            return await _context.SchoolInfos.ToListAsync();
        }

        // GET: api/SchoolInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolInfo>> GetSchoolInfo(int id)
        {
            var schoolInfo = await _context.SchoolInfos.FindAsync(id);

            if (schoolInfo == null)
            {
                return NotFound();
            }

            return schoolInfo;
        }

        // PUT: api/SchoolInfoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchoolInfo(int id, SchoolInfo schoolInfo)
        {
            if (id != schoolInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(schoolInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolInfoExists(id))
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

        // POST: api/SchoolInfoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SchoolInfo>> PostSchoolInfo(SchoolInfo schoolInfo)
        {
            _context.SchoolInfos.Add(schoolInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchoolInfo", new { id = schoolInfo.Id }, schoolInfo);
        }

        // DELETE: api/SchoolInfoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SchoolInfo>> DeleteSchoolInfo(int id)
        {
            var schoolInfo = await _context.SchoolInfos.FindAsync(id);
            if (schoolInfo == null)
            {
                return NotFound();
            }

            _context.SchoolInfos.Remove(schoolInfo);
            await _context.SaveChangesAsync();

            return schoolInfo;
        }

        private bool SchoolInfoExists(int id)
        {
            return _context.SchoolInfos.Any(e => e.Id == id);
        }
    }
}
