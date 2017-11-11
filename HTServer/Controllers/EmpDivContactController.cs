using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HTServer.Models;
using HTServer.Filters;
using Microsoft.AspNetCore.Cors;

namespace HTServer.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Produces("application/json")]
    [Route("api/EmpDivContact")]
    //[TypeFilter(typeof(APIUserAuthorizeAttribute))]

    public class EmpDivContactController : Controller
    {
        // GET api/EmpDivContact
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new EmpDivContactQuery(db);
                var result = await query.LatestPostsAsync();
                return new OkObjectResult(result);
            }
        }

        // GET api/EmpDivContact/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new EmpDivContactQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                return new OkObjectResult(result);
            }
        }

        // POST api/EmpDivContact
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]EmpDivContact body)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                body.Db = db;
                await body.InsertAsync();
                return new OkObjectResult(body);
            }
        }

        // PUT api/EmpDivContact/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody]EmpDivContact body)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new EmpDivContactQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                result.EmpContactId = body.EmpContactId;
                result.EmpId = body.EmpId;
                await result.UpdateAsync();
                return new OkObjectResult(result);
            }
        }

        // DELETE api/EmpDivContact/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new EmpDivContactQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                await result.DeleteAsync();
                return new OkResult();
            }
        }
    }
}