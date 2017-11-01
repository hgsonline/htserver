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
    [Route("api/ActMbrBasePremiums")]
    public class ActMbrBasePremiumsController : Controller
    {
        private readonly HTDataContext _context;

        public ActMbrBasePremiumsController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/ActMbrBasePremiums
        [HttpGet]
        public IEnumerable<ActMbrBasePremium> Getactmbrbasepremium()
        {
            return _context.actmbrbasepremium;
        }

        // GET: api/ActMbrBasePremiums/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActMbrBasePremium([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actMbrBasePremium = await _context.actmbrbasepremium.SingleOrDefaultAsync(m => m.MbrBasePremID == id);

            if (actMbrBasePremium == null)
            {
                return NotFound();
            }

            return Ok(actMbrBasePremium);
        }

        // PUT: api/ActMbrBasePremiums/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActMbrBasePremium([FromRoute] int id, [FromBody] ActMbrBasePremium actMbrBasePremium)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != actMbrBasePremium.MbrBasePremID)
            {
                return BadRequest();
            }

            _context.Entry(actMbrBasePremium).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActMbrBasePremiumExists(id))
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

        // POST: api/ActMbrBasePremiums
        [HttpPost]
        public async Task<IActionResult> PostActMbrBasePremium([FromBody] ActMbrBasePremium actMbrBasePremium)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.actmbrbasepremium.Add(actMbrBasePremium);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActMbrBasePremium", new { id = actMbrBasePremium.MbrBasePremID }, actMbrBasePremium);
        }

        // DELETE: api/ActMbrBasePremiums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActMbrBasePremium([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actMbrBasePremium = await _context.actmbrbasepremium.SingleOrDefaultAsync(m => m.MbrBasePremID == id);
            if (actMbrBasePremium == null)
            {
                return NotFound();
            }

            _context.actmbrbasepremium.Remove(actMbrBasePremium);
            await _context.SaveChangesAsync();

            return Ok(actMbrBasePremium);
        }

        private bool ActMbrBasePremiumExists(int id)
        {
            return _context.actmbrbasepremium.Any(e => e.MbrBasePremID == id);
        }
    }
}