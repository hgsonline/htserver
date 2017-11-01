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
    [Route("api/PrvMedFacDivProvider")]
    public class PrvMedFacDivProviderController : Controller
    {
        private readonly HTDataContext _context;

        public PrvMedFacDivProviderController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/PrvMedFacDivProvider
        [HttpGet]
        public IEnumerable<PrvMedFacDivProvider> Getprvmedfacdivprovider()
        {
            return _context.prvmedfacdivprovider;
        }

        // GET: api/PrvMedFacDivProvider/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrvMedFacDivProvider([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prvMedFacDivProvider = await _context.prvmedfacdivprovider.SingleOrDefaultAsync(m => m.MedFacDivPrvID == id);

            if (prvMedFacDivProvider == null)
            {
                return NotFound();
            }

            return Ok(prvMedFacDivProvider);
        }

        // PUT: api/PrvMedFacDivProvider/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrvMedFacDivProvider([FromRoute] int id, [FromBody] PrvMedFacDivProvider prvMedFacDivProvider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prvMedFacDivProvider.MedFacDivPrvID)
            {
                return BadRequest();
            }

            _context.Entry(prvMedFacDivProvider).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrvMedFacDivProviderExists(id))
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

        // POST: api/PrvMedFacDivProvider
        [HttpPost]
        public async Task<IActionResult> PostPrvMedFacDivProvider([FromBody] PrvMedFacDivProvider prvMedFacDivProvider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.prvmedfacdivprovider.Add(prvMedFacDivProvider);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrvMedFacDivProvider", new { id = prvMedFacDivProvider.MedFacDivPrvID }, prvMedFacDivProvider);
        }

        // DELETE: api/PrvMedFacDivProvider/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrvMedFacDivProvider([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prvMedFacDivProvider = await _context.prvmedfacdivprovider.SingleOrDefaultAsync(m => m.MedFacDivPrvID == id);
            if (prvMedFacDivProvider == null)
            {
                return NotFound();
            }

            _context.prvmedfacdivprovider.Remove(prvMedFacDivProvider);
            await _context.SaveChangesAsync();

            return Ok(prvMedFacDivProvider);
        }

        private bool PrvMedFacDivProviderExists(int id)
        {
            return _context.prvmedfacdivprovider.Any(e => e.MedFacDivPrvID == id);
        }
    }
}