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
    [Route("api/States")]
    public class StatesController : Controller
    {
        private readonly HTDataContext _context;

        public StatesController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/States
        [HttpGet]
        public IEnumerable<States> Getstates()
        {
            return _context.states;
        }

        // GET: api/States/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStates([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var states = await _context.states.SingleOrDefaultAsync(m => m.id == id);

            if (states == null)
            {
                return NotFound();
            }

            return Ok(states);
        }

        // PUT: api/States/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStates([FromRoute] int id, [FromBody] States states)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != states.id)
            {
                return BadRequest();
            }

            _context.Entry(states).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatesExists(id))
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

        // POST: api/States
        [HttpPost]
        public async Task<IActionResult> PostStates([FromBody] States states)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.states.Add(states);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStates", new { id = states.id }, states);
        }

        // DELETE: api/States/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStates([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var states = await _context.states.SingleOrDefaultAsync(m => m.id == id);
            if (states == null)
            {
                return NotFound();
            }

            _context.states.Remove(states);
            await _context.SaveChangesAsync();

            return Ok(states);
        }

        private bool StatesExists(int id)
        {
            return _context.states.Any(e => e.id == id);
        }
    }
}