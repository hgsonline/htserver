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
    [Route("api/EmpEEFamilies")]
    public class EmpEEFamiliesController : Controller
    {
        private readonly HTDataContext _context;

        public EmpEEFamiliesController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/EmpEEFamilies
        [HttpGet]
        public IEnumerable<EmpEEFamily> Getempeefamily()
        {
            return _context.empeefamily;
        }

        // GET: api/EmpEEFamilies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpEEFamily([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empEEFamily = await _context.empeefamily.SingleOrDefaultAsync(m => m.EEFamilyID == id);

            if (empEEFamily == null)
            {
                return NotFound();
            }

            return Ok(empEEFamily);
        }

        // PUT: api/EmpEEFamilies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpEEFamily([FromRoute] int id, [FromBody] EmpEEFamily empEEFamily)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empEEFamily.EEFamilyID)
            {
                return BadRequest();
            }

            _context.Entry(empEEFamily).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpEEFamilyExists(id))
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

        // POST: api/EmpEEFamilies
        [HttpPost]
        public async Task<IActionResult> PostEmpEEFamily([FromBody] EmpEEFamily empEEFamily)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.empeefamily.Add(empEEFamily);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpEEFamily", new { id = empEEFamily.EEFamilyID }, empEEFamily);
        }

        // DELETE: api/EmpEEFamilies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpEEFamily([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empEEFamily = await _context.empeefamily.SingleOrDefaultAsync(m => m.EEFamilyID == id);
            if (empEEFamily == null)
            {
                return NotFound();
            }

            _context.empeefamily.Remove(empEEFamily);
            await _context.SaveChangesAsync();

            return Ok(empEEFamily);
        }

        private bool EmpEEFamilyExists(int id)
        {
            return _context.empeefamily.Any(e => e.EEFamilyID == id);
        }
    }
}