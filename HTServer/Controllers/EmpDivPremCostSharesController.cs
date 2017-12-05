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
    [Route("api/EmpDivPremCostShares")]
    public class EmpDivPremCostSharesController : Controller
    {
        private readonly HTDataContext _context;

        public EmpDivPremCostSharesController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/EmpDivPremCostShares
        [HttpGet]
        public IEnumerable<EmpDivPremCostShare> Getempdivpremcostshare()
        {
            return _context.empdivpremcostshare;
        }

        // GET: api/EmpDivPremCostShares/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpDivPremCostShare([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empDivPremCostShare = await _context.empdivpremcostshare.SingleOrDefaultAsync(m => m.EmpPremCostShareID == id);

            if (empDivPremCostShare == null)
            {
                return NotFound();
            }

            return Ok(empDivPremCostShare);
        }

        // PUT: api/EmpDivPremCostShares/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpDivPremCostShare([FromRoute] int id, [FromBody] EmpDivPremCostShare empDivPremCostShare)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empDivPremCostShare.EmpPremCostShareID)
            {
                return BadRequest();
            }

            _context.Entry(empDivPremCostShare).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpDivPremCostShareExists(id))
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

        // POST: api/EmpDivPremCostShares
        [HttpPost]
        public async Task<IActionResult> PostEmpDivPremCostShare([FromBody] EmpDivPremCostShare empDivPremCostShare)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.empdivpremcostshare.Add(empDivPremCostShare);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpDivPremCostShare", new { id = empDivPremCostShare.EmpPremCostShareID }, empDivPremCostShare);
        }

        // DELETE: api/EmpDivPremCostShares/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpDivPremCostShare([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empDivPremCostShare = await _context.empdivpremcostshare.SingleOrDefaultAsync(m => m.EmpPremCostShareID == id);
            if (empDivPremCostShare == null)
            {
                return NotFound();
            }

            _context.empdivpremcostshare.Remove(empDivPremCostShare);
            await _context.SaveChangesAsync();

            return Ok(empDivPremCostShare);
        }

        private bool EmpDivPremCostShareExists(int id)
        {
            return _context.empdivpremcostshare.Any(e => e.EmpPremCostShareID == id);
        }
    }
}