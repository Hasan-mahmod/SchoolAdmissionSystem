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
    public class AdmitCardsController : ControllerBase
    {
        private readonly DbAdmissionSystem _context;

        public AdmitCardsController(DbAdmissionSystem context)
        {
            _context = context;
        }

        // GET: api/AdmitCards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdmitCard>>> GetAdmitCards()
        {
            return await _context.AdmitCards.ToListAsync();
        }

        // GET: api/AdmitCards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdmitCard>> GetAdmitCard(int id)
        {
            var admitCard = await _context.AdmitCards.FindAsync(id);

            if (admitCard == null)
            {
                return NotFound();
            }

            return admitCard;
        }

        // PUT: api/AdmitCards/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmitCard(int id, AdmitCard admitCard)
        {
            if (id != admitCard.Id)
            {
                return BadRequest();
            }

            _context.Entry(admitCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdmitCardExists(id))
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

        // POST: api/AdmitCards
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AdmitCard>> PostAdmitCard(AdmitCard admitCard)
        {
            _context.AdmitCards.Add(admitCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdmitCard", new { id = admitCard.Id }, admitCard);
        }

        // DELETE: api/AdmitCards/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AdmitCard>> DeleteAdmitCard(int id)
        {
            var admitCard = await _context.AdmitCards.FindAsync(id);
            if (admitCard == null)
            {
                return NotFound();
            }

            _context.AdmitCards.Remove(admitCard);
            await _context.SaveChangesAsync();

            return admitCard;
        }

        private bool AdmitCardExists(int id)
        {
            return _context.AdmitCards.Any(e => e.Id == id);
        }
    }
}
