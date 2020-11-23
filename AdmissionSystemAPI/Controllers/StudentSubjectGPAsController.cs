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
    public class StudentSubjectGPAsController : ControllerBase
    {
        private readonly DbAdmissionSystem _context;

        public StudentSubjectGPAsController(DbAdmissionSystem context)
        {
            _context = context;
        }

        // GET: api/StudentSubjectGPAs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentSubjectGPA>>> GetStudentSubjectGPAs()
        {
            return await _context.StudentSubjectGPAs.ToListAsync();
        }

        // GET: api/StudentSubjectGPAs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentSubjectGPA>> GetStudentSubjectGPA(int id)
        {
            var studentSubjectGPA = await _context.StudentSubjectGPAs.FindAsync(id);

            if (studentSubjectGPA == null)
            {
                return NotFound();
            }

            return studentSubjectGPA;
        }

        // PUT: api/StudentSubjectGPAs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentSubjectGPA(int id, StudentSubjectGPA studentSubjectGPA)
        {
            if (id != studentSubjectGPA.Id)
            {
                return BadRequest();
            }

            _context.Entry(studentSubjectGPA).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentSubjectGPAExists(id))
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

        // POST: api/StudentSubjectGPAs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<StudentSubjectGPA>> PostStudentSubjectGPA(StudentSubjectGPA studentSubjectGPA)
        {
            _context.StudentSubjectGPAs.Add(studentSubjectGPA);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudentSubjectGPA", new { id = studentSubjectGPA.Id }, studentSubjectGPA);
        }

        // DELETE: api/StudentSubjectGPAs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentSubjectGPA>> DeleteStudentSubjectGPA(int id)
        {
            var studentSubjectGPA = await _context.StudentSubjectGPAs.FindAsync(id);
            if (studentSubjectGPA == null)
            {
                return NotFound();
            }

            _context.StudentSubjectGPAs.Remove(studentSubjectGPA);
            await _context.SaveChangesAsync();

            return studentSubjectGPA;
        }

        private bool StudentSubjectGPAExists(int id)
        {
            return _context.StudentSubjectGPAs.Any(e => e.Id == id);
        }
    }
}
