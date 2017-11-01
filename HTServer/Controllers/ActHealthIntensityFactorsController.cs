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
    [Route("api/ActHealthIntensityFactors")]
    public class ActHealthIntensityFactorsController : Controller
    {
        private readonly HTDataContext _context;

        public ActHealthIntensityFactorsController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/ActHealthIntensityFactors
        [HttpGet]
        public IEnumerable<ActHealthIntensityFactor> Getacthealthintensityfactor()
        {
            return _context.acthealthintensityfactor;
        }

        // GET: api/ActHealthIntensityFactors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActHealthIntensityFactor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actHealthIntensityFactor = await _context.acthealthintensityfactor.SingleOrDefaultAsync(m => m.HealthIntensityFactorID == id);

            if (actHealthIntensityFactor == null)
            {
                return NotFound();
            }

            return Ok(actHealthIntensityFactor);
        }

        // PUT: api/ActHealthIntensityFactors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActHealthIntensityFactor([FromRoute] int id, [FromBody] ActHealthIntensityFactor actHealthIntensityFactor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != actHealthIntensityFactor.HealthIntensityFactorID)
            {
                return BadRequest();
            }

            _context.Entry(actHealthIntensityFactor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActHealthIntensityFactorExists(id))
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

        // POST: api/ActHealthIntensityFactors
        [HttpPost]
        public async Task<IActionResult> PostActHealthIntensityFactor([FromBody] ActHealthIntensityFactor actHealthIntensityFactor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.acthealthintensityfactor.Add(actHealthIntensityFactor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActHealthIntensityFactor", new { id = actHealthIntensityFactor.HealthIntensityFactorID }, actHealthIntensityFactor);
        }

        // DELETE: api/ActHealthIntensityFactors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActHealthIntensityFactor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actHealthIntensityFactor = await _context.acthealthintensityfactor.SingleOrDefaultAsync(m => m.HealthIntensityFactorID == id);
            if (actHealthIntensityFactor == null)
            {
                return NotFound();
            }

            _context.acthealthintensityfactor.Remove(actHealthIntensityFactor);
            await _context.SaveChangesAsync();

            return Ok(actHealthIntensityFactor);
        }

        private bool ActHealthIntensityFactorExists(int id)
        {
            return _context.acthealthintensityfactor.Any(e => e.HealthIntensityFactorID == id);
        }
    }
}