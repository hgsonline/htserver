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
    [Route("api/ActEmployerPremiumCostShares")]
    public class ActEmployerPremiumCostSharesController : Controller
    {
        private readonly HTDataContext _context;

        public ActEmployerPremiumCostSharesController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/ActEmployerPremiumCostShares
        [HttpGet]
        public IEnumerable<ActEmployerPremiumCostShare> Getactemployerpremiumcostshare()
        {
            return _context.actemployerpremiumcostshare;
        }

        // GET: api/ActEmployerPremiumCostShares/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActEmployerPremiumCostShare([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actEmployerPremiumCostShare = await _context.actemployerpremiumcostshare.SingleOrDefaultAsync(m => m.EmpPremCostShareID == id);

            if (actEmployerPremiumCostShare == null)
            {
                return NotFound();
            }

            return Ok(actEmployerPremiumCostShare);
        }

        // PUT: api/ActEmployerPremiumCostShares/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActEmployerPremiumCostShare([FromRoute] int id, [FromBody] ActEmployerPremiumCostShare actEmployerPremiumCostShare)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != actEmployerPremiumCostShare.EmpPremCostShareID)
            {
                return BadRequest();
            }

            _context.Entry(actEmployerPremiumCostShare).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActEmployerPremiumCostShareExists(id))
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

        // POST: api/ActEmployerPremiumCostShares
        [HttpPost]
        public async Task<IActionResult> PostActEmployerPremiumCostShare([FromBody] ActEmployerPremiumCostShare actEmployerPremiumCostShare)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.actemployerpremiumcostshare.Add(actEmployerPremiumCostShare);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActEmployerPremiumCostShare", new { id = actEmployerPremiumCostShare.EmpPremCostShareID }, actEmployerPremiumCostShare);
        }

        // DELETE: api/ActEmployerPremiumCostShares/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActEmployerPremiumCostShare([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actEmployerPremiumCostShare = await _context.actemployerpremiumcostshare.SingleOrDefaultAsync(m => m.EmpPremCostShareID == id);
            if (actEmployerPremiumCostShare == null)
            {
                return NotFound();
            }

            _context.actemployerpremiumcostshare.Remove(actEmployerPremiumCostShare);
            await _context.SaveChangesAsync();

            return Ok(actEmployerPremiumCostShare);
        }

        private bool ActEmployerPremiumCostShareExists(int id)
        {
            return _context.actemployerpremiumcostshare.Any(e => e.EmpPremCostShareID == id);
        }
    }
}