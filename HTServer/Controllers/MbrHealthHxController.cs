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
    [Route("api/MbrHealthHx")]
    public class MbrHealthHxController : Controller
    {
        private readonly HTDataContext _context;

        public MbrHealthHxController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/MbrHealthHx
        [HttpGet]
        public IEnumerable<MbrHealthHx> Getmbrhealthhx()
        {
            return _context.mbrhealthhx;
        }

        // GET: api/MbrHealthHx/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMbrHealthHx([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mbrHealthHx = await _context.mbrhealthhx.SingleOrDefaultAsync(m => m.MbrHealthHxID == id);

            if (mbrHealthHx == null)
            {
                return NotFound();
            }

            return Ok(mbrHealthHx);
        }

        // PUT: api/MbrHealthHx/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMbrHealthHx([FromRoute] int id, [FromBody] MbrHealthHx mbrHealthHx)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mbrHealthHx.MbrHealthHxID)
            {
                return BadRequest();
            }

            _context.Entry(mbrHealthHx).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MbrHealthHxExists(id))
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

        // POST: api/MbrHealthHx
        [HttpPost]
        public async Task<IActionResult> PostMbrHealthHx([FromBody] MbrHealthHx mbrHealthHx)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.mbrhealthhx.Add(mbrHealthHx);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMbrHealthHx", new { id = mbrHealthHx.MbrHealthHxID }, mbrHealthHx);
        }

        // DELETE: api/MbrHealthHx/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMbrHealthHx([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mbrHealthHx = await _context.mbrhealthhx.SingleOrDefaultAsync(m => m.MbrHealthHxID == id);
            if (mbrHealthHx == null)
            {
                return NotFound();
            }

            _context.mbrhealthhx.Remove(mbrHealthHx);
            await _context.SaveChangesAsync();

            return Ok(mbrHealthHx);
        }

        private bool MbrHealthHxExists(int id)
        {
            return _context.mbrhealthhx.Any(e => e.MbrHealthHxID == id);
        }
    }
}