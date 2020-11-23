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
    public class ExamInfoesController : ControllerBase
    {
        private readonly DbAdmissionSystem _context;

        public ExamInfoesController(DbAdmissionSystem context)
        {
            _context = context;
        }

        // GET: api/ExamInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamInfo>>> GetExamInfos()
        {
            return await _context.ExamInfos.ToListAsync();
        }

        // GET: api/ExamInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamInfo>> GetExamInfo(int id)
        {
            var examInfo = await _context.ExamInfos.FindAsync(id);

            if (examInfo == null)
            {
                return NotFound();
            }

            return examInfo;
        }

        // PUT: api/ExamInfoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamInfo(int id, ExamInfo examInfo)
        {
            if (id != examInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(examInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamInfoExists(id))
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

        // POST: api/ExamInfoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ExamInfo>> PostExamInfo(ExamInfo examInfo)
        {
            _context.ExamInfos.Add(examInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExamInfo", new { id = examInfo.Id }, examInfo);
        }

        // DELETE: api/ExamInfoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExamInfo>> DeleteExamInfo(int id)
        {
            var examInfo = await _context.ExamInfos.FindAsync(id);
            if (examInfo == null)
            {
                return NotFound();
            }

            _context.ExamInfos.Remove(examInfo);
            await _context.SaveChangesAsync();

            return examInfo;
        }

        private bool ExamInfoExists(int id)
        {
            return _context.ExamInfos.Any(e => e.Id == id);
        }
    }
}
