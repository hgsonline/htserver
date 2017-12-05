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
    [Route("api/EmpDivWageTypes")]
    public class EmpDivWageTypesController : Controller
    {
        private readonly HTDataContext _context;

        public EmpDivWageTypesController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/EmpDivWageTypes
        [HttpGet]
        public IEnumerable<EmpDivWageType> Getempdivwagetype()
        {
            return _context.empdivwagetype;
        }

        // GET: api/EmpDivWageTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpDivWageType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empDivWageType = await _context.empdivwagetype.SingleOrDefaultAsync(m => m.EmpDivWageTypeID == id);

            if (empDivWageType == null)
            {
                return NotFound();
            }

            return Ok(empDivWageType);
        }

        // PUT: api/EmpDivWageTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpDivWageType([FromRoute] int id, [FromBody] EmpDivWageType empDivWageType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empDivWageType.EmpDivWageTypeID)
            {
                return BadRequest();
            }

            _context.Entry(empDivWageType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpDivWageTypeExists(id))
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

        // POST: api/EmpDivWageTypes
        [HttpPost]
        public async Task<IActionResult> PostEmpDivWageType([FromBody] EmpDivWageType empDivWageType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.empdivwagetype.Add(empDivWageType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpDivWageType", new { id = empDivWageType.EmpDivWageTypeID }, empDivWageType);
        }

        // DELETE: api/EmpDivWageTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpDivWageType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empDivWageType = await _context.empdivwagetype.SingleOrDefaultAsync(m => m.EmpDivWageTypeID == id);
            if (empDivWageType == null)
            {
                return NotFound();
            }

            _context.empdivwagetype.Remove(empDivWageType);
            await _context.SaveChangesAsync();

            return Ok(empDivWageType);
        }

        private bool EmpDivWageTypeExists(int id)
        {
            return _context.empdivwagetype.Any(e => e.EmpDivWageTypeID == id);
        }
    }
}