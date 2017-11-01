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
    [Route("api/PrvProviderContractDetails")]
    public class PrvProviderContractDetailsController : Controller
    {
        private readonly HTDataContext _context;

        public PrvProviderContractDetailsController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/PrvProviderContractDetails
        [HttpGet]
        public IEnumerable<PrvProviderContractDetail> Getprvprovidercontractdetail()
        {
            return _context.prvprovidercontractdetail;
        }

        // GET: api/PrvProviderContractDetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrvProviderContractDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prvProviderContractDetail = await _context.prvprovidercontractdetail.SingleOrDefaultAsync(m => m.ProviderContractDetailID == id);

            if (prvProviderContractDetail == null)
            {
                return NotFound();
            }

            return Ok(prvProviderContractDetail);
        }

        // PUT: api/PrvProviderContractDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrvProviderContractDetail([FromRoute] int id, [FromBody] PrvProviderContractDetail prvProviderContractDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prvProviderContractDetail.ProviderContractDetailID)
            {
                return BadRequest();
            }

            _context.Entry(prvProviderContractDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrvProviderContractDetailExists(id))
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

        // POST: api/PrvProviderContractDetails
        [HttpPost]
        public async Task<IActionResult> PostPrvProviderContractDetail([FromBody] PrvProviderContractDetail prvProviderContractDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.prvprovidercontractdetail.Add(prvProviderContractDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrvProviderContractDetail", new { id = prvProviderContractDetail.ProviderContractDetailID }, prvProviderContractDetail);
        }

        // DELETE: api/PrvProviderContractDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrvProviderContractDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prvProviderContractDetail = await _context.prvprovidercontractdetail.SingleOrDefaultAsync(m => m.ProviderContractDetailID == id);
            if (prvProviderContractDetail == null)
            {
                return NotFound();
            }

            _context.prvprovidercontractdetail.Remove(prvProviderContractDetail);
            await _context.SaveChangesAsync();

            return Ok(prvProviderContractDetail);
        }

        private bool PrvProviderContractDetailExists(int id)
        {
            return _context.prvprovidercontractdetail.Any(e => e.ProviderContractDetailID == id);
        }
    }
}