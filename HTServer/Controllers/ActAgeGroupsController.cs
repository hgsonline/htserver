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
    [Route("api/ActAgeGroups")]
    public class ActAgeGroupsController : Controller
    {
        private readonly HTDataContext _context;

        public ActAgeGroupsController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/ActAgeGroups
        [HttpGet]
        public IEnumerable<ActAgeGroup> Getactagegroup()
        {
            return _context.actagegroup;
        }

        // GET: api/ActAgeGroups/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActAgeGroup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actAgeGroup = await _context.actagegroup.SingleOrDefaultAsync(m => m.AgeGroupID == id);

            if (actAgeGroup == null)
            {
                return NotFound();
            }

            return Ok(actAgeGroup);
        }

        // PUT: api/ActAgeGroups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActAgeGroup([FromRoute] int id, [FromBody] ActAgeGroup actAgeGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != actAgeGroup.AgeGroupID)
            {
                return BadRequest();
            }

            _context.Entry(actAgeGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActAgeGroupExists(id))
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

        // POST: api/ActAgeGroups
        [HttpPost]
        public async Task<IActionResult> PostActAgeGroup([FromBody] ActAgeGroup actAgeGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.actagegroup.Add(actAgeGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActAgeGroup", new { id = actAgeGroup.AgeGroupID }, actAgeGroup);
        }

        // DELETE: api/ActAgeGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActAgeGroup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actAgeGroup = await _context.actagegroup.SingleOrDefaultAsync(m => m.AgeGroupID == id);
            if (actAgeGroup == null)
            {
                return NotFound();
            }

            _context.actagegroup.Remove(actAgeGroup);
            await _context.SaveChangesAsync();

            return Ok(actAgeGroup);
        }

        private bool ActAgeGroupExists(int id)
        {
            return _context.actagegroup.Any(e => e.AgeGroupID == id);
        }
    }
}