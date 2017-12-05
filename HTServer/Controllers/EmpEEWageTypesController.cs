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
    [Route("api/EmpEEWageTypes")]
    public class EmpEEWageTypesController : Controller
    {
        private readonly HTDataContext _context;

        public EmpEEWageTypesController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/EmpEEWageTypes
        [HttpGet]
        public IEnumerable<EmpEEWageType> Getempeewagetype()
        {
            return _context.empeewagetype;
        }

        // GET: api/EmpEEWageTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpEEWageType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empEEWageType = await _context.empeewagetype.SingleOrDefaultAsync(m => m.EEWageTypeID == id);

            if (empEEWageType == null)
            {
                return NotFound();
            }

            return Ok(empEEWageType);
        }

        // PUT: api/EmpEEWageTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpEEWageType([FromRoute] int id, [FromBody] EmpEEWageType empEEWageType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empEEWageType.EEWageTypeID)
            {
                return BadRequest();
            }

            _context.Entry(empEEWageType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpEEWageTypeExists(id))
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

        // POST: api/EmpEEWageTypes
        [HttpPost]
        public async Task<IActionResult> PostEmpEEWageType([FromBody] EmpEEWageType empEEWageType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.empeewagetype.Add(empEEWageType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpEEWageType", new { id = empEEWageType.EEWageTypeID }, empEEWageType);
        }

        // DELETE: api/EmpEEWageTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpEEWageType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empEEWageType = await _context.empeewagetype.SingleOrDefaultAsync(m => m.EEWageTypeID == id);
            if (empEEWageType == null)
            {
                return NotFound();
            }

            _context.empeewagetype.Remove(empEEWageType);
            await _context.SaveChangesAsync();

            return Ok(empEEWageType);
        }

        private bool EmpEEWageTypeExists(int id)
        {
            return _context.empeewagetype.Any(e => e.EEWageTypeID == id);
        }
    }
}