using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HTServer.Models;
using HTServer.Filters;
using HTServer.Commonlibary;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;

namespace HTServer.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Produces("application/json")]
    [Route("api/EmpEmployerDiv")]
    //[TypeFilter(typeof(APIUserAuthorizeAttribute))]

    public class EmpEmployerDivController : Controller
    {

        private readonly IEmailService _emailService;
        private readonly HTDataContext _context;

        public EmpEmployerDivController(IEmailService emailService, HTDataContext context)
        {
            _emailService = emailService;
            _context = context;
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
        [HttpGet("GetOne/{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new EmpEmployerDivQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                return new OkObjectResult(result);
            }
        }

        // GET api/EmpEmployerDiv/5
        [HttpGet("{eid}")]
        public async Task<IActionResult> GetEmployer(int eid)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new EmpEmployerDivQuery(db);
                var result = await query.FindEmployerAsync(eid);
                if (result == null)
                    return new NotFoundResult();
                return new OkObjectResult(result);
            }
        }

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
                result.EmpDivID = body.EmpDivID;
                result.NAICS_ID = body.NAICS_ID;
                result.DivisionRelation = body.DivisionRelation;
                result.DBAName = body.DBAName;
                result.ContactPriority = body.ContactPriority;
                result.PositionTitle = body.PositionTitle;
                result.LastName = body.LastName;
                result.FirstName = body.FirstName;
                result.MiddleName = body.MiddleName;
                result.NameSuffix = body.NameSuffix;
                result.EmailAddress = body.EmailAddress;
                result.PostalCode = body.PostalCode;
                result.StateProvince = body.StateProvince;
                result.City = body.City;
                result.Street1 = body.Street1;
                result.Street2 = body.Street2;
                result.EntityTypeID = body.EntityTypeID;
    
                await result.UpdateAsync();
                return new OkObjectResult(body);
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