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
    [Route("api/ActEECostShareFactors")]
    public class ActEECostShareFactorsController : Controller
    {
        private readonly HTDataContext _context;

        public ActEECostShareFactorsController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/ActEECostShareFactors
        [HttpGet]
        public IEnumerable<ActEECostShareFactor> Getacteecostsharefactor()
        {
            return _context.acteecostsharefactor;
        }

        // GET: api/ActEECostShareFactors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActEECostShareFactor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actEECostShareFactor = await _context.acteecostsharefactor.SingleOrDefaultAsync(m => m.EECostShareFactorID == id);

            if (actEECostShareFactor == null)
            {
                return NotFound();
            }

            return Ok(actEECostShareFactor);
        }

        // PUT: api/ActEECostShareFactors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActEECostShareFactor([FromRoute] int id, [FromBody] ActEECostShareFactor actEECostShareFactor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != actEECostShareFactor.EECostShareFactorID)
            {
                return BadRequest();
            }

            _context.Entry(actEECostShareFactor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActEECostShareFactorExists(id))
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

        // POST: api/ActEECostShareFactors
        [HttpPost]
        public async Task<IActionResult> PostActEECostShareFactor([FromBody] ActEECostShareFactor actEECostShareFactor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.acteecostsharefactor.Add(actEECostShareFactor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActEECostShareFactor", new { id = actEECostShareFactor.EECostShareFactorID }, actEECostShareFactor);
        }

        // DELETE: api/ActEECostShareFactors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActEECostShareFactor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actEECostShareFactor = await _context.acteecostsharefactor.SingleOrDefaultAsync(m => m.EECostShareFactorID == id);
            if (actEECostShareFactor == null)
            {
                return NotFound();
            }

            _context.acteecostsharefactor.Remove(actEECostShareFactor);
            await _context.SaveChangesAsync();

            return Ok(actEECostShareFactor);
        }

        private bool ActEECostShareFactorExists(int id)
        {
            return _context.acteecostsharefactor.Any(e => e.EECostShareFactorID == id);
        }
    }
}