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
    [Route("api/MbrFamilyHealthHx")]
    public class MbrFamilyHealthHxController : Controller
    {
        private readonly HTDataContext _context;

        public MbrFamilyHealthHxController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/MbrFamilyHealthHx
        [HttpGet]
        public IEnumerable<MbrFamilyHealthHx> Getmbrfamilyhealthhx()
        {
            return _context.mbrfamilyhealthhx;
        }

        // GET: api/MbrFamilyHealthHx/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMbrFamilyHealthHx([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mbrfamilyHealthHx = await _context.mbrfamilyhealthhx.SingleOrDefaultAsync(m => m.FamilyHealthHxID == id);

            if (mbrfamilyHealthHx == null)
            {
                return NotFound();
            }

            return Ok(mbrfamilyHealthHx);
        }

        // PUT: api/MbrFamilyHealthHx/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMbrFamilyHealthHx([FromRoute] int id, [FromBody] MbrFamilyHealthHx mbrfamilyHealthHx)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mbrfamilyHealthHx.FamilyHealthHxID)
            {
                return BadRequest();
            }

            _context.Entry(mbrfamilyHealthHx).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MbrFamilyHealthHxExists(id))
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

        // POST: api/MbrFamilyHealthHx
        [HttpPost]
        public async Task<IActionResult> PostMbrFamilyHealthHx([FromBody] MbrFamilyHealthHx mbrfamilyHealthHx)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.mbrfamilyhealthhx.Add(mbrfamilyHealthHx);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMbrFamilyHealthHx", new { id = mbrfamilyHealthHx.FamilyHealthHxID }, mbrfamilyHealthHx);
        }

        // DELETE: api/MbrFamilyHealthHx/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMbrFamilyHealthHx([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mbrfamilyHealthHx = await _context.mbrfamilyhealthhx.SingleOrDefaultAsync(m => m.FamilyHealthHxID == id);
            if (mbrfamilyHealthHx == null)
            {
                return NotFound();
            }

            _context.mbrfamilyhealthhx.Remove(mbrfamilyHealthHx);
            await _context.SaveChangesAsync();

            return Ok(mbrfamilyHealthHx);
        }

        private bool MbrFamilyHealthHxExists(int id)
        {
            return _context.mbrfamilyhealthhx.Any(e => e.FamilyHealthHxID == id);
        }
    }
}