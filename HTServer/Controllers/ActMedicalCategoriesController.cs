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
    [Route("api/ActMedicalCategories")]
    public class ActMedicalCategoriesController : Controller
    {
        private readonly HTDataContext _context;

        public ActMedicalCategoriesController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/ActMedicalCategories
        [HttpGet]
        public IEnumerable<ActMedicalCategory> Getactmedicalcategory()
        {
            return _context.actmedicalcategory;
        }

        // GET: api/ActMedicalCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActMedicalCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actMedicalCategory = await _context.actmedicalcategory.SingleOrDefaultAsync(m => m.MedicalCategoryID == id);

            if (actMedicalCategory == null)
            {
                return NotFound();
            }

            return Ok(actMedicalCategory);
        }

        // PUT: api/ActMedicalCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActMedicalCategory([FromRoute] int id, [FromBody] ActMedicalCategory actMedicalCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != actMedicalCategory.MedicalCategoryID)
            {
                return BadRequest();
            }

            _context.Entry(actMedicalCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActMedicalCategoryExists(id))
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

        // POST: api/ActMedicalCategories
        [HttpPost]
        public async Task<IActionResult> PostActMedicalCategory([FromBody] ActMedicalCategory actMedicalCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.actmedicalcategory.Add(actMedicalCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActMedicalCategory", new { id = actMedicalCategory.MedicalCategoryID }, actMedicalCategory);
        }

        // DELETE: api/ActMedicalCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActMedicalCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actMedicalCategory = await _context.actmedicalcategory.SingleOrDefaultAsync(m => m.MedicalCategoryID == id);
            if (actMedicalCategory == null)
            {
                return NotFound();
            }

            _context.actmedicalcategory.Remove(actMedicalCategory);
            await _context.SaveChangesAsync();

            return Ok(actMedicalCategory);
        }

        private bool ActMedicalCategoryExists(int id)
        {
            return _context.actmedicalcategory.Any(e => e.MedicalCategoryID == id);
        }
    }
}