using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HTServer.Models;

namespace HTServer.Controllers
{
    [Produces("application/json")]
    [Route("api/PrvMedFacDivision")]
    public class PrvMedFacDivisionController : Controller
    {
        private readonly HTDataContext _context;

        public PrvMedFacDivisionController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/PrvMedFacDivision
        [HttpGet]
        public IEnumerable<PrvMedFacDivision> Getprvmedfacdivision()
        {
            return _context.prvmedfacdivision;
        }

        // GET: api/PrvMedFacDivision/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrvMedFacDivision([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prvMedFacDivision = await _context.prvmedfacdivision.SingleOrDefaultAsync(m => m.MedFacDivID == id);

            if (prvMedFacDivision == null)
            {
                return NotFound();
            }

            return Ok(prvMedFacDivision);
        }

        // PUT: api/PrvMedFacDivision/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrvMedFacDivision([FromRoute] int id, [FromBody] PrvMedFacDivision prvMedFacDivision)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prvMedFacDivision.MedFacDivID)
            {
                return BadRequest();
            }

            _context.Entry(prvMedFacDivision).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrvMedFacDivisionExists(id))
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

        // POST: api/PrvMedFacDivision
        [HttpPost]
        public async Task<IActionResult> PostPrvMedFacDivision([FromBody] PrvMedFacDivision prvMedFacDivision)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.prvmedfacdivision.Add(prvMedFacDivision);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrvMedFacDivision", new { id = prvMedFacDivision.MedFacDivID }, prvMedFacDivision);
        }

        // DELETE: api/PrvMedFacDivision/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrvMedFacDivision([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prvMedFacDivision = await _context.prvmedfacdivision.SingleOrDefaultAsync(m => m.MedFacDivID == id);
            if (prvMedFacDivision == null)
            {
                return NotFound();
            }

            _context.prvmedfacdivision.Remove(prvMedFacDivision);
            await _context.SaveChangesAsync();

            return Ok(prvMedFacDivision);
        }

        private bool PrvMedFacDivisionExists(int id)
        {
            return _context.prvmedfacdivision.Any(e => e.MedFacDivID == id);
        }
    }
}