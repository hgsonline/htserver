using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HTServer.Models;
using HTServer.Filters;
using HTServer.Commonlibary;

namespace HTServer.Controllers
{
    [Produces("application/json")]
    [Route("api/PrvProvider")]

    public class PrvProviderController : Controller
    {
        private readonly IEmailService _emailService;

        public PrvProviderController(IEmailService emailService)
        {
            _emailService = emailService;
        }


        // GET api/PrvProvider
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new PrvProviderQuery(db);
                var result = await query.LatestPostsAsync();
                return new OkObjectResult(result);
            }
        }

        // GET api/PrvProvider/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new PrvProviderQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                return new OkObjectResult(result);
            }
        }


        // POST api/PrvProvider
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PrvProvider body)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                body.Db = db;
                await body.InsertAsync();


                //return new OkObjectResult(body);


                var query = new PrvProviderQuery(db);
                var result = await query.FindOneAsync(body.ProviderID);
                if (result == null)
                    return new NotFoundResult();

                await _emailService.SendEmailAsync("saip@hgsonline.net", "Welcome to Health Trust " , "Welcome to Health Trust!<br/><br/>     You just registered to Provider Module on Health Trust.<br/><br/>     <b>Credential Details</b><br/><br/>     User Name: " + result.AccountId + " <br/>     Provider ID: "+ result.ProviderID  + " <br/><br/>     Password is your last 6 digit number of your User Name.<br/><br/>Regards.<br/>Healt Trust Team.");
                
                return new OkObjectResult(result);
            }
        }

    }
}