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
    [Route("api/EmpDefaultWageTypes")]
    public class EmpDefaultWageTypesController : Controller
    {
        private readonly HTDataContext _context;

        public EmpDefaultWageTypesController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/EmpDefaultWageTypes
        [HttpGet]
        public IEnumerable<EmpDefaultWageType> Getempdefaultwagetype()
        {
            return _context.empdefaultwagetype;
        }

        // GET: api/EmpDefaultWageTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpDefaultWageType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empDefaultWageType = await _context.empdefaultwagetype.SingleOrDefaultAsync(m => m.DefaultWageTypeID == id);

            if (empDefaultWageType == null)
            {
                return NotFound();
            }

            return Ok(empDefaultWageType);
        }

        // PUT: api/EmpDefaultWageTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpDefaultWageType([FromRoute] int id, [FromBody] EmpDefaultWageType empDefaultWageType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empDefaultWageType.DefaultWageTypeID)
            {
                return BadRequest();
            }

            _context.Entry(empDefaultWageType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpDefaultWageTypeExists(id))
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

        // POST: api/EmpDefaultWageTypes
        [HttpPost]
        public async Task<IActionResult> PostEmpDefaultWageType([FromBody] EmpDefaultWageType empDefaultWageType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.empdefaultwagetype.Add(empDefaultWageType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpDefaultWageType", new { id = empDefaultWageType.DefaultWageTypeID }, empDefaultWageType);
        }

        // DELETE: api/EmpDefaultWageTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpDefaultWageType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empDefaultWageType = await _context.empdefaultwagetype.SingleOrDefaultAsync(m => m.DefaultWageTypeID == id);
            if (empDefaultWageType == null)
            {
                return NotFound();
            }

            _context.empdefaultwagetype.Remove(empDefaultWageType);
            await _context.SaveChangesAsync();

            return Ok(empDefaultWageType);
        }

        private bool EmpDefaultWageTypeExists(int id)
        {
            return _context.empdefaultwagetype.Any(e => e.DefaultWageTypeID == id);
        }
    }
}