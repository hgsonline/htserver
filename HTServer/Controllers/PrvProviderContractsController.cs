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
    [Route("api/PrvProviderContracts")]
    public class PrvProviderContractsController : Controller
    {
        private readonly HTDataContext _context;

        public PrvProviderContractsController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/PrvProviderContracts
        [HttpGet]
        public IEnumerable<PrvProviderContract> Getprvprovidercontract()
        {
            return _context.prvprovidercontract;
        }

        // GET: api/PrvProviderContracts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrvProviderContract([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prvProviderContract = await _context.prvprovidercontract.SingleOrDefaultAsync(m => m.ProviderContractID == id);

            if (prvProviderContract == null)
            {
                return NotFound();
            }

            return Ok(prvProviderContract);
        }

        // PUT: api/PrvProviderContracts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrvProviderContract([FromRoute] int id, [FromBody] PrvProviderContract prvProviderContract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prvProviderContract.ProviderContractID)
            {
                return BadRequest();
            }

            _context.Entry(prvProviderContract).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrvProviderContractExists(id))
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

        // POST: api/PrvProviderContracts
        [HttpPost]
        public async Task<IActionResult> PostPrvProviderContract([FromBody] PrvProviderContract prvProviderContract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.prvprovidercontract.Add(prvProviderContract);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrvProviderContract", new { id = prvProviderContract.ProviderContractID }, prvProviderContract);
        }

        // DELETE: api/PrvProviderContracts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrvProviderContract([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prvProviderContract = await _context.prvprovidercontract.SingleOrDefaultAsync(m => m.ProviderContractID == id);
            if (prvProviderContract == null)
            {
                return NotFound();
            }

            _context.prvprovidercontract.Remove(prvProviderContract);
            await _context.SaveChangesAsync();

            return Ok(prvProviderContract);
        }

        private bool PrvProviderContractExists(int id)
        {
            return _context.prvprovidercontract.Any(e => e.ProviderContractID == id);
        }
    }
}