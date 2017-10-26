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
    [Route("api/MbrPremPMPMs")]
    public class MbrPremPMPMsController : Controller
    {
        private readonly HTDataContext _context;

        public MbrPremPMPMsController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/MbrPremPMPMs
        [HttpGet]
        public IEnumerable<MbrPremPMPM> Getmbrprempmpm()
        {
            return _context.mbrprempmpm;
        }

        // GET: api/MbrPremPMPMs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMbrPremPMPM([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mbrPremPMPM = await _context.mbrprempmpm.SingleOrDefaultAsync(m => m.MbrPremPMPMID == id);

            if (mbrPremPMPM == null)
            {
                return NotFound();
            }

            return Ok(mbrPremPMPM);
        }

        // PUT: api/MbrPremPMPMs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMbrPremPMPM([FromRoute] int id, [FromBody] MbrPremPMPM mbrPremPMPM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mbrPremPMPM.MbrPremPMPMID)
            {
                return BadRequest();
            }

            _context.Entry(mbrPremPMPM).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MbrPremPMPMExists(id))
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

        // POST: api/MbrPremPMPMs
        [HttpPost]
        public async Task<IActionResult> PostMbrPremPMPM([FromBody] MbrPremPMPM mbrPremPMPM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.mbrprempmpm.Add(mbrPremPMPM);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMbrPremPMPM", new { id = mbrPremPMPM.MbrPremPMPMID }, mbrPremPMPM);
        }

        // DELETE: api/MbrPremPMPMs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMbrPremPMPM([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mbrPremPMPM = await _context.mbrprempmpm.SingleOrDefaultAsync(m => m.MbrPremPMPMID == id);
            if (mbrPremPMPM == null)
            {
                return NotFound();
            }

            _context.mbrprempmpm.Remove(mbrPremPMPM);
            await _context.SaveChangesAsync();

            return Ok(mbrPremPMPM);
        }

        private bool MbrPremPMPMExists(int id)
        {
            return _context.mbrprempmpm.Any(e => e.MbrPremPMPMID == id);
        }
    }
}