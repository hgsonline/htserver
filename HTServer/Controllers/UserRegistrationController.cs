using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HTServer.Models;
using MySql.Data.MySqlClient;
//using Pomelo.Data.MySql;

namespace HTServer.Controllers
{
    [Produces("application/json")]
    [Route("api/UserRegistration")]
    public class UserRegistrationController : Controller
    {

        // POST api/empemployer
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserRegistration body)
        {
            using (var db = new AppDb())
            {
                try
                {
                    await db.Connection.OpenAsync();
                    body.Db = db;
                    await body.InsertAsync();
                    return new OkObjectResult(body);
                }
                catch (MySqlException e)
                {
                    return new OkObjectResult(e.Message.ToString());
                }
            }
        }
    }
}