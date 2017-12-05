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
    [Route("api/EmpEmpDivEEs")]
    public class EmpEmpDivEEsController : Controller
    {
        private readonly HTDataContext _context;

        public EmpEmpDivEEsController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/EmpEmpDivEEs
        [HttpGet]
        public IEnumerable<EmpEmpDivEE> Getempempdivee()
        {
            return _context.empempdivee;
        }

        // GET: api/EmpEmpDivEEs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpEmpDivEE([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empEmpDivEE = await _context.empempdivee.SingleOrDefaultAsync(m => m.EmpDivEEID == id);

            if (empEmpDivEE == null)
            {
                return NotFound();
            }

            return Ok(empEmpDivEE);
        }

        // PUT: api/EmpEmpDivEEs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpEmpDivEE([FromRoute] int id, [FromBody] EmpEmpDivEE empEmpDivEE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empEmpDivEE.EmpDivEEID)
            {
                return BadRequest();
            }

            _context.Entry(empEmpDivEE).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpEmpDivEEExists(id))
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

        // POST: api/EmpEmpDivEEs
        [HttpPost]
        public async Task<IActionResult> PostEmpEmpDivEE([FromBody] EmpEmpDivEE empEmpDivEE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.empempdivee.Add(empEmpDivEE);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpEmpDivEE", new { id = empEmpDivEE.EmpDivEEID }, empEmpDivEE);
        }

        // DELETE: api/EmpEmpDivEEs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpEmpDivEE([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empEmpDivEE = await _context.empempdivee.SingleOrDefaultAsync(m => m.EmpDivEEID == id);
            if (empEmpDivEE == null)
            {
                return NotFound();
            }

            _context.empempdivee.Remove(empEmpDivEE);
            await _context.SaveChangesAsync();

            return Ok(empEmpDivEE);
        }

        private bool EmpEmpDivEEExists(int id)
        {
            return _context.empempdivee.Any(e => e.EmpDivEEID == id);
        }
    }
}