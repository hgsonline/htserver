using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HTServer.Models;
using HTServer.Filters;
using HTServer.Commonlibary;

namespace HTServer.Controllers
{
    [Produces("application/json")]
    [Route("api/EmpEmployerDiv")]
    //[TypeFilter(typeof(APIUserAuthorizeAttribute))]

    public class EmpEmployerDivController : Controller
    {

        private readonly IEmailService _emailService;

        public EmpEmployerDivController(IEmailService emailService)
        {
            _emailService = emailService;
        }


        // GET api/EmpEmployerDiv
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new EmpEmployerDivQuery(db);
                var result = await query.LatestPostsAsync();
                return new OkObjectResult(result);
            }
        }

        // GET api/EmpEmployerDiv/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new EmpEmployerDivQuery(db);
                var result = await query.FindEmployerAsync(id);
                if (result == null)
                    return new NotFoundResult();
                return new OkObjectResult(result);
            }
        }

        // GET api/EmpEmployerDiv/5
        //[HttpGet("{eid}")]
        //public async Task<IActionResult> GetEmployer(int eid)
        //{
        //    using (var db = new AppDb())
        //    {
        //        await db.Connection.OpenAsync();
        //        var query = new EmpEmployerDivQuery(db);
        //        var result = await query.FindEmployerAsync(eid);
        //        if (result == null)
        //            return new NotFoundResult();
        //        return new OkObjectResult(result);
        //    }
        //}

        // POST api/EmpEmployerDiv
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]EmpEmployerDiv body)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                body.Db = db;
                await body.InsertAsync();

            
                var query = new EmpEmployerDivQuery(db);
                var result = await query.FindOneAsync(body.EmpId);
                if (result == null)
                    return new NotFoundResult();
                if (result.EmpDivID == 0)
                { 
                await _emailService.SendEmailAsync("saip@hgsonline.net" , "Welcome to Health Trust ", "Welcome to Health Trust!<br/><br/>     You just registered to Employer Module on Health Trust.<br/><br/>     <b>Credential Details</b><br/><br/>     User Name: " + result.AccountId + " <br/>     Employer ID: " + result.EmpId  + " <br/><br/>     Password is your last 6 digit number of your User Name.<br/><br/>Regards.<br/>Healt Trust Team.");
                }

                return new OkObjectResult(result);
            }
        }

        // PUT api/EmpEmployerDiv/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody]EmpEmployerDiv body)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new EmpEmployerDivQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                result.IRS_EIN = body.IRS_EIN;
                result.CompanyName = body.CompanyName;
                await result.UpdateAsync();
                return new OkObjectResult(result);
            }
        }

        // DELETE api/EmpEmployerDiv/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new EmpEmployerDivQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                await result.DeleteAsync();
                return new OkResult();
            }
        }

    }
}