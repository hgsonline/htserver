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
    [Route("api/EmpEECostShareFactors")]
    public class EmpEECostShareFactorsController : Controller
    {
        private readonly HTDataContext _context;

        public EmpEECostShareFactorsController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/EmpEECostShareFactors
        [HttpGet]
        public IEnumerable<EmpEECostShareFactor> Getempeecostsharefactor()
        {
            return _context.empeecostsharefactor;
        }

        // GET: api/EmpEECostShareFactors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpEECostShareFactor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empEECostShareFactor = await _context.empeecostsharefactor.SingleOrDefaultAsync(m => m.EECostShareFactorID == id);

            if (empEECostShareFactor == null)
            {
                return NotFound();
            }

            return Ok(empEECostShareFactor);
        }

        // PUT: api/EmpEECostShareFactors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpEECostShareFactor([FromRoute] int id, [FromBody] EmpEECostShareFactor empEECostShareFactor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empEECostShareFactor.EECostShareFactorID)
            {
                return BadRequest();
            }

            _context.Entry(empEECostShareFactor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpEECostShareFactorExists(id))
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

        // POST: api/EmpEECostShareFactors
        [HttpPost]
        public async Task<IActionResult> PostEmpEECostShareFactor([FromBody] EmpEECostShareFactor empEECostShareFactor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.empeecostsharefactor.Add(empEECostShareFactor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpEECostShareFactor", new { id = empEECostShareFactor.EECostShareFactorID }, empEECostShareFactor);
        }

        // DELETE: api/EmpEECostShareFactors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpEECostShareFactor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empEECostShareFactor = await _context.empeecostsharefactor.SingleOrDefaultAsync(m => m.EECostShareFactorID == id);
            if (empEECostShareFactor == null)
            {
                return NotFound();
            }

            _context.empeecostsharefactor.Remove(empEECostShareFactor);
            await _context.SaveChangesAsync();

            return Ok(empEECostShareFactor);
        }

        private bool EmpEECostShareFactorExists(int id)
        {
            return _context.empeecostsharefactor.Any(e => e.EECostShareFactorID == id);
        }
    }
}