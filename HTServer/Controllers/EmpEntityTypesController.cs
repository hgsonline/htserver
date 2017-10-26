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
    [Route("api/EmpEntityTypes")]
    public class EmpEntityTypesController : Controller
    {
        private readonly HTDataContext _context;

        public EmpEntityTypesController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/EmpEntityTypes
        [HttpGet]
        public IEnumerable<EmpEntityType> GetEmpEntityType()
        {
            return _context.empentitytype;
        }

        // GET: api/EmpEntityTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpEntityType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empEntityType = await _context.empentitytype.SingleOrDefaultAsync(m => m.EntityTypeID == id);

            if (empEntityType == null)
            {
                return NotFound();
            }

            return Ok(empEntityType);
        }

        // PUT: api/EmpEntityTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpEntityType([FromRoute] int id, [FromBody] EmpEntityType empEntityType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empEntityType.EntityTypeID)
            {
                return BadRequest();
            }

            _context.Entry(empEntityType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpEntityTypeExists(id))
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

        // POST: api/EmpEntityTypes
        [HttpPost]
        public async Task<IActionResult> PostEmpEntityType([FromBody] EmpEntityType empEntityType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.empentitytype.Add(empEntityType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpEntityType", new { id = empEntityType.EntityTypeID }, empEntityType);
        }

        // DELETE: api/EmpEntityTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpEntityType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empEntityType = await _context.empentitytype.SingleOrDefaultAsync(m => m.EntityTypeID == id);
            if (empEntityType == null)
            {
                return NotFound();
            }

            _context.empentitytype.Remove(empEntityType);
            await _context.SaveChangesAsync();

            return Ok(empEntityType);
        }

        private bool EmpEntityTypeExists(int id)
        {
            return _context.empentitytype.Any(e => e.EntityTypeID == id);
        }
    }
}