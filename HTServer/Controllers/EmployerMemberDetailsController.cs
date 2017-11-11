using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HTServer.Models;

namespace HTServer.Controllers
{
    [Produces("application/json")]
    [Route("api/EmployerMemberDetails")]
    public class EmployerMemberDetailsController : Controller
    {
        [HttpGet("{eid}")]
        public async Task<IActionResult> GetOne(int eid)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new EmpMemberDepQuery(db);
                var result = await query.FindEmployerMemberAsync(eid);
                if (result == null)
                    return new NotFoundResult();
                return new OkObjectResult(result);
            }
        }
    }
}