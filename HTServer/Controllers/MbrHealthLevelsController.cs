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
    [Route("api/MbrHealthLevels")]
    public class MbrHealthLevelsController : Controller
    {
        private readonly HTDataContext _context;

        public MbrHealthLevelsController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/MbrHealthLevels
        [HttpGet]
        public IEnumerable<MbrHealthLevel> Getmbrhealthlevel()
        {
            return _context.mbrhealthlevel;
        }

        // GET: api/MbrHealthLevels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMbrHealthLevel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mbrHealthLevel = await _context.mbrhealthlevel.SingleOrDefaultAsync(m => m.MbrHealthLevelID == id);

            if (mbrHealthLevel == null)
            {
                return NotFound();
            }

            return Ok(mbrHealthLevel);
        }

        // PUT: api/MbrHealthLevels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMbrHealthLevel([FromRoute] int id, [FromBody] MbrHealthLevel mbrHealthLevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mbrHealthLevel.MbrHealthLevelID)
            {
                return BadRequest();
            }

            _context.Entry(mbrHealthLevel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MbrHealthLevelExists(id))
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

        // POST: api/MbrHealthLevels
        [HttpPost]
        public async Task<IActionResult> PostMbrHealthLevel([FromBody] MbrHealthLevel mbrHealthLevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.mbrhealthlevel.Add(mbrHealthLevel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMbrHealthLevel", new { id = mbrHealthLevel.MbrHealthLevelID }, mbrHealthLevel);
        }

        // DELETE: api/MbrHealthLevels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMbrHealthLevel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mbrHealthLevel = await _context.mbrhealthlevel.SingleOrDefaultAsync(m => m.MbrHealthLevelID == id);
            if (mbrHealthLevel == null)
            {
                return NotFound();
            }

            _context.mbrhealthlevel.Remove(mbrHealthLevel);
            await _context.SaveChangesAsync();

            return Ok(mbrHealthLevel);
        }

        private bool MbrHealthLevelExists(int id)
        {
            return _context.mbrhealthlevel.Any(e => e.MbrHealthLevelID == id);
        }
    }
}