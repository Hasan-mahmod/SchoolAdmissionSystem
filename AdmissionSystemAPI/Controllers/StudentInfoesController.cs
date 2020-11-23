using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdmissionSystemAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.Emit;
using AdmissionSystemAPI.ExtraCs;

namespace AdmissionSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentInfoesController : ControllerBase
    {
        private readonly DbAdmissionSystem _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private IHostingEnvironment _ev;
        public StudentInfoesController(DbAdmissionSystem context, UserManager<User> userManager, RoleManager<Role> roleManager, IHostingEnvironment ev)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _ev = ev;
        }

        // GET: api/StudentInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentInfo>>> GetStudentInfos()
        {
            return await _context.StudentInfos.ToListAsync();
        }

        // GET: api/StudentInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentInfo>> GetStudentInfo(string email)
        {
            
            var id = await _context.StudentInfos.Where(s=>s.Email==email).Select(p=>p.Id).SingleOrDefaultAsync();
            var studentInfo = _context.StudentInfos.Find(id);

            if (studentInfo == null)
            {
                return NotFound();
            }
            List<ApplyForm> applyforms;
            using (_context)
            {
                applyforms = _context.ApplyForms.Where(p => p.StudentId == id).ToList();
            }
            studentInfo.ApplyForms = applyforms;
            return studentInfo;
        }

        // PUT: api/StudentInfoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentInfo(int id, StudentInfo studentInfo)
        {
            if (id != studentInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(studentInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentInfoExists(id))
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

        // POST: api/StudentInfoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<StudentInfo>> PostStudentInfo(StudentInfo studentInfo)
        //{
        //    _context.StudentInfos.Add(studentInfo);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetStudentInfo", new { id = studentInfo.Id }, studentInfo);
        //}


        [HttpPost]
        public async Task<IActionResult> PostStudentInfo([FromBody] StudentInfo studentInfo, IFormFile photo, IFormFile sign)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            OtherHelper oh = new OtherHelper();
            string path = _ev.WebRootPath;
            if (photo != null)
            {
                string photofolder = path + "public/studentimage";
                var ph = oh.SaveFil(photo, photofolder, studentInfo.Photo);
                studentInfo.Photo = ph;
            }
            if (sign != null)
            {
                string signfolder = path + "public/studentsign";
                var si = oh.SaveFil(sign, signfolder, studentInfo.Signature);
                studentInfo.Signature = si;
            }

            bool IstransactionComplete = false;
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (studentInfo.Id > 0)
                    {
                        _context.Entry(studentInfo).State = EntityState.Modified;
                    }
                    else
                    {
                        _context.StudentInfos.Add(studentInfo);
                        var user = new User
                        {
                            UserName = studentInfo.Email,
                            Email = studentInfo.Email
                        };
                        var pass = "@Test123";
                        var result = await _userManager.CreateAsync(user, pass);
                        if (result.Succeeded)
                        {
                            string mess = string.Format("{0} Account created successfuly . Your Usen Name : {1} and Password : {2}", studentInfo.StudentName, user.UserName, pass);
                            var p = oh.Sendmail(user.Email, "Account Registration Success", mess);

                        }
                        _context.SaveChanges();
                        transaction.Commit();
                        IstransactionComplete = true;
                    }

                }
                catch (Exception e)
                {
                    IstransactionComplete = false;
                    return Problem(e.Message);

                }
            }
            _context.StudentInfos.Add(studentInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudentInfo", new { id = studentInfo.Id, status = IstransactionComplete }, studentInfo);
        }


        // DELETE: api/StudentInfoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentInfo>> DeleteStudentInfo(int id)
        {
            var studentInfo = await _context.StudentInfos.FindAsync(id);
            if (studentInfo == null)
            {
                return NotFound();
            }

            _context.StudentInfos.Remove(studentInfo);
            await _context.SaveChangesAsync();

            return studentInfo;
        }

        private bool StudentInfoExists(int id)
        {
            return _context.StudentInfos.Any(e => e.Id == id);
        }
    }
}
