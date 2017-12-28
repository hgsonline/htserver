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
    [Route("api/EmpDepMedicalData")]
    //[TypeFilter(typeof(APIUserAuthorizeAttribute))]

    public class EmpDepMedicalDataController : Controller
    {
        // GET api/EmpDepMedicalData
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new EmpDepMedicalDataQuery(db);
                var result = await query.LatestPostsAsync();
                return new OkObjectResult(result);
            }
        }

        // GET api/EmpDepMedicalData/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new EmpDepMedicalDataQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                return new OkObjectResult(result);
            }
        }

        // POST api/EmpDepMedicalData
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]EmpDepMedicalData body)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                body.Db = db;
                await body.InsertOrUpdateAsync();
                var query = new EmpDepMedicalDataQuery(db);
                var result = await query.FindOneAsync(body.MemberID);
                if (result == null)
                    return new NotFoundResult();

                return new OkObjectResult(result);
            }
        }

        // PUT api/EmpMemberDep/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody]EmpDepMedicalData body)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new EmpDepMedicalDataQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();

                result.MemberID = body.MemberID;
                result.IsSmoker = body.IsSmoker;
                result.SmokerFor = body.SmokerFor;
                result.SmokingYrs = body.SmokingYrs;
                result.IsDrinker = body.IsDrinker;
                result.DrinkerFor = body.DrinkerFor;
                result.DrinkingYrs = body.DrinkingYrs;
                result.IsDiabetic = body.IsDiabetic;
                result.IsRelDiabetic = body.IsRelDiabetic;
                result.HasHeartDisease = body.HasHeartDisease;
                result.HasRelHeartDisease = body.HasRelHeartDisease;
                result.IsHBP = body.IsHBP;
                result.IsRelHBP = body.IsRelHBP;
                result.HasStomachDisorder = body.HasStomachDisorder;
                result.HasRelStomachDisorder = body.HasRelStomachDisorder;
                result.HasLungDisorder = body.HasLungDisorder;
                result.HasRelLungDisorder = body.HasRelLungDisorder;
                result.HasCancer = body.HasCancer;
                result.HasRelCancer = body.HasRelCancer;

                result.HasKidneyDisease = body.HasKidneyDisease;
                result.HasRelKidneyDisease = body.HasRelKidneyDisease;
                result.HasThyroidDisease = body.HasThyroidDisease;
                result.HasRelThyroidDisease = body.HasRelThyroidDisease;
                result.HasStroke = body.HasStroke;
                result.HasRelStroke = body.HasRelStroke;
                result.HasRhumatoidDisease = body.HasRhumatoidDisease;
                result.HasRelRhumatoidDisease = body.HasRelRhumatoidDisease;
                result.HasDegenDisorders = body.HasStomachDisorder;
                result.HasRelDegenDisorders = body.HasRelDegenDisorders;
                result.HasSiezureDisorder = body.HasSiezureDisorder;
                result.HasRelSiezureDisorder = body.HasRelSiezureDisorder;
                result.HasHepatitis = body.HasHepatitis;
                result.HasRelHepatitis = body.HasRelHepatitis;

                await result.UpdateAsync();
                return new OkObjectResult(result);
            }
        }

        // DELETE api/EmpDepMedicalData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new EmpDepMedicalDataQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                await result.DeleteAsync();
                return new OkResult();
            }
        }

    }
}