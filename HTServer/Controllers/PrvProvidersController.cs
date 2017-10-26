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
    [Route("api/PrvProviders")]
    public class PrvProvidersController : Controller
    {
        private readonly HTDataContext _context;

        public PrvProvidersController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/PrvProviders
        [HttpGet]
        public IEnumerable<PrvProvider> Getprvprovider()
        {
            return _context.prvprovider;
        }

        // GET: api/PrvProviders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrvProvider([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prvProvider = await _context.prvprovider.SingleOrDefaultAsync(m => m.ProviderID == id);

            if (prvProvider == null)
            {
                return NotFound();
            }

            return Ok(prvProvider);
        }

        // PUT: api/PrvProviders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrvProvider([FromRoute] int id, [FromBody] PrvProvider prvProvider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prvProvider.ProviderID)
            {
                return BadRequest();
            }

            _context.Entry(prvProvider).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrvProviderExists(id))
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

        // POST: api/PrvProviders
        [HttpPost]
        public async Task<IActionResult> PostPrvProvider([FromBody] PrvProvider prvProvider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.prvprovider.Add(prvProvider);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrvProvider", new { id = prvProvider.ProviderID }, prvProvider);
        }

        // DELETE: api/PrvProviders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrvProvider([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prvProvider = await _context.prvprovider.SingleOrDefaultAsync(m => m.ProviderID == id);
            if (prvProvider == null)
            {
                return NotFound();
            }

            _context.prvprovider.Remove(prvProvider);
            await _context.SaveChangesAsync();

            return Ok(prvProvider);
        }

        private bool PrvProviderExists(int id)
        {
            return _context.prvprovider.Any(e => e.ProviderID == id);
        }
    }
}