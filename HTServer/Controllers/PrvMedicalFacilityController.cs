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
    [Route("api/PrvMedicalFacility")]
    public class PrvMedicalFacilityController : Controller
    {
        private readonly HTDataContext _context;

        public PrvMedicalFacilityController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/PrvMedicalFacility
        [HttpGet]
        public IEnumerable<PrvMedicalFacility> Getprvmedicalfacility()
        {
            return _context.prvmedicalfacility;
        }

        // GET: api/PrvMedicalFacility/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrvMedicalFacility([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prvMedicalFacility = await _context.prvmedicalfacility.SingleOrDefaultAsync(m => m.MedFacID == id);

            if (prvMedicalFacility == null)
            {
                return NotFound();
            }

            return Ok(prvMedicalFacility);
        }

        // PUT: api/PrvMedicalFacility/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrvMedicalFacility([FromRoute] int id, [FromBody] PrvMedicalFacility prvMedicalFacility)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prvMedicalFacility.MedFacID)
            {
                return BadRequest();
            }

            _context.Entry(prvMedicalFacility).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrvMedicalFacilityExists(id))
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

        // POST: api/PrvMedicalFacility
        [HttpPost]
        public async Task<IActionResult> PostPrvMedicalFacility([FromBody] PrvMedicalFacility prvMedicalFacility)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.prvmedicalfacility.Add(prvMedicalFacility);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrvMedicalFacility", new { id = prvMedicalFacility.MedFacID }, prvMedicalFacility);
        }

        // DELETE: api/PrvMedicalFacility/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrvMedicalFacility([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prvMedicalFacility = await _context.prvmedicalfacility.SingleOrDefaultAsync(m => m.MedFacID == id);
            if (prvMedicalFacility == null)
            {
                return NotFound();
            }

            _context.prvmedicalfacility.Remove(prvMedicalFacility);
            await _context.SaveChangesAsync();

            return Ok(prvMedicalFacility);
        }

        private bool PrvMedicalFacilityExists(int id)
        {
            return _context.prvmedicalfacility.Any(e => e.MedFacID == id);
        }
    }
}