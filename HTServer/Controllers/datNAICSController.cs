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
    [Route("api/datNAICS")]
    public class datNAICSController : Controller
    {
        private readonly HTDataContext _context;

        public datNAICSController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/datNAICS
        [HttpGet]
        public IEnumerable<shortCodes> GetdatNAICS()
        {
            List<shortCodes> naicsList = (from item in _context.datNAICS
                             select new shortCodes()
                             {
                                 Id = item.Code,
                                 Name = item.Code + " ," + item.Sector + "," + item.SubSector + " ," + item.IndustryGroup
                             }).ToList();
            return naicsList;
        }

        // GET: api/datNAICS/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetdatNAICS([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var datNAICS = await _context.datNAICS.SingleOrDefaultAsync(m => m.Code == id);

            if (datNAICS == null)
            {
                return NotFound();
            }

            return Ok(datNAICS);
        }

        // PUT: api/datNAICS/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutdatNAICS([FromRoute] int id, [FromBody] datNAICS datNAICS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != datNAICS.Code)
            {
                return BadRequest();
            }

            _context.Entry(datNAICS).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!datNAICSExists(id))
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

        // POST: api/datNAICS
        [HttpPost]
        public async Task<IActionResult> PostdatNAICS([FromBody] datNAICS datNAICS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.datNAICS.Add(datNAICS);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetdatNAICS", new { id = datNAICS.Code }, datNAICS);
        }

        // DELETE: api/datNAICS/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletedatNAICS([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var datNAICS = await _context.datNAICS.SingleOrDefaultAsync(m => m.Code == id);
            if (datNAICS == null)
            {
                return NotFound();
            }

            _context.datNAICS.Remove(datNAICS);
            await _context.SaveChangesAsync();

            return Ok(datNAICS);
        }

        private bool datNAICSExists(int id)
        {
            return _context.datNAICS.Any(e => e.Code == id);
        }
    }
}