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
    [Route("api/PrvMedFacDivEntityType")]
    public class PrvMedFacDivEntityTypeController : Controller
    {
        private readonly HTDataContext _context;

        public PrvMedFacDivEntityTypeController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/PrvMedFacDivEntityType
        [HttpGet]
        public IEnumerable<PrvMedFacDivEntityType> Getprvmedfacdiventitytype()
        {
            return _context.prvmedfacdiventitytype;
        }

        // GET: api/PrvMedFacDivEntityType/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrvMedFacDivEntityType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prvMedFacDivEntityType = await _context.prvmedfacdiventitytype.SingleOrDefaultAsync(m => m.MedFacDivEntityTypeID == id);

            if (prvMedFacDivEntityType == null)
            {
                return NotFound();
            }

            return Ok(prvMedFacDivEntityType);
        }

        // PUT: api/PrvMedFacDivEntityType/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrvMedFacDivEntityType([FromRoute] int id, [FromBody] PrvMedFacDivEntityType prvMedFacDivEntityType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prvMedFacDivEntityType.MedFacDivEntityTypeID)
            {
                return BadRequest();
            }

            _context.Entry(prvMedFacDivEntityType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrvMedFacDivEntityTypeExists(id))
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

        // POST: api/PrvMedFacDivEntityType
        [HttpPost]
        public async Task<IActionResult> PostPrvMedFacDivEntityType([FromBody] PrvMedFacDivEntityType prvMedFacDivEntityType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.prvmedfacdiventitytype.Add(prvMedFacDivEntityType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrvMedFacDivEntityType", new { id = prvMedFacDivEntityType.MedFacDivEntityTypeID }, prvMedFacDivEntityType);
        }

        // DELETE: api/PrvMedFacDivEntityType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrvMedFacDivEntityType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prvMedFacDivEntityType = await _context.prvmedfacdiventitytype.SingleOrDefaultAsync(m => m.MedFacDivEntityTypeID == id);
            if (prvMedFacDivEntityType == null)
            {
                return NotFound();
            }

            _context.prvmedfacdiventitytype.Remove(prvMedFacDivEntityType);
            await _context.SaveChangesAsync();

            return Ok(prvMedFacDivEntityType);
        }

        private bool PrvMedFacDivEntityTypeExists(int id)
        {
            return _context.prvmedfacdiventitytype.Any(e => e.MedFacDivEntityTypeID == id);
        }
    }
}