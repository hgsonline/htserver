using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HTServer.Models;
using HTServer.Filters;
using HTServer.Commonlibary;
using Microsoft.AspNetCore.Cors;

namespace HTServer.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Produces("application/json")] 
    [Route("api/EmpMemberDep")]
    //[TypeFilter(typeof(APIUserAuthorizeAttribute))]

    public class EmpMemberDepController : Controller
    {
        private readonly IEmailService _emailService;

        public EmpMemberDepController(IEmailService emailService)
        {
            _emailService = emailService;
        }



        // GET api/EmpMemberDep
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new EmpMemberDepQuery(db);
                var result = await query.LatestPostsAsync();
                return new OkObjectResult(result);
            }
        }

        // GET api/EmpMemberDep/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new EmpMemberDepQuery(db);
                var result = await query.FindMemberAsync(id);
                if (result == null)
                    return new NotFoundResult();
                return new OkObjectResult(result);
            }
        }

        // GET api/EmpMemberDep/5
        //[HttpGet("{mid}")]
        //public async Task<IActionResult> GetMember(int mid)
        //{
        //    using (var db = new AppDb())
        //    {
        //        await db.Connection.OpenAsync();
        //        var query = new EmpMemberDepQuery(db);
        //        var result = await query.FindMemberAsync(mid);
        //        if (result == null)
        //            return new NotFoundResult();
        //        return new OkObjectResult(result);
        //    }
        //}
        
        // POST api/EmpMemberDep
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]EmpMemberDep body)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                body.Db = db;
                await body.InsertAsync();
                //return new OkObjectResult(body);



                var query = new EmpMemberDepQuery(db);
                var result = await query.FindOneAsync(body.MemberID);
                if (result == null)
                    return new NotFoundResult();

                if (result.DependentID == 0)
                {
                    await _emailService.SendEmailAsync("saip@hgsonline.net", "Welcome to Health Trust ", "Welcome to Health Trust!<br/><br/>     You just registered to Member Module on Health Trust.<br/><br/>     <b>Credential Details</b><br/><br/>     User Name: " + result.AccountId + " <br/>     Employer ID: " + result.MemberID  + " <br/><br/>     Password is your last 6 digit number of your User Name.<br/><br/>Regards.<br/>Healt Trust Team.");
                }

                return new OkObjectResult(result);
            }
        }

        // PUT api/EmpMemberDep/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody]EmpMemberDep body)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new EmpMemberDepQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                result.EmpId = body.EmpId;
                result.FirstName = body.FirstName;
                await result.UpdateAsync();
                return new OkObjectResult(result);
            }
        }

        // DELETE api/EmpMemberDep/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new EmpMemberDepQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                await result.DeleteAsync();
                return new OkResult();
            }
        }

    }
}