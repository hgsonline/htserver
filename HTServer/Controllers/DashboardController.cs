using System;
using System.Collections.Generic;
using System.Linq;
using HTServer.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

namespace HTServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Dashboard")]
    public class DashboardController : Controller
    {
        private readonly HTDataContext _context;

        public DashboardController(HTDataContext context)
        {
            _context = context;
        }

        // GET: api/GetEmployerDashboardCount
        [HttpGet("Employer/{id}")]
        public async Task<Dashboard> GetEmployerDashboardCount(int id)
        {
            
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var cmd = db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"getEmployerDashboardCount";
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = @"SELECT * FROM `empemployerdiv` WHERE `EmpId` = @id or `EmpDivID` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
            }
        }
        // GET: api/GetEmployerDashboardCount
        [HttpGet("Member/{id}")]
        public async Task<Dashboard> GetMemberDashboardCount(int id)
        {

            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var cmd = db.Connection.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"getMemberDashboardCount";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = @"SELECT * FROM `empemployerdiv` WHERE `EmpId` = @id or `EmpDivID` = @id";
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "@id",
                    DbType = DbType.Int32,
                    Value = id,
                });
                return await ReadAllAsync(await cmd.ExecuteReaderAsync());
            }
        }

        private async Task<Dashboard> ReadAllAsync(DbDataReader reader)
        {
            var post = new Dashboard();
            using (reader)
            {
                while (await reader.ReadAsync())
                {

                    post.EmpId = await reader.GetFieldValueAsync<int>(0);
                    post.TotalDivisions = await reader.IsDBNullAsync(1) ? 0 : await reader.GetFieldValueAsync<Int64>(1);
                    post.TotalMembers = await reader.IsDBNullAsync(2) ? 0 : await reader.GetFieldValueAsync<Int64>(2);
                    post.CopayAmount = await reader.IsDBNullAsync(3) ? 0 : await reader.GetFieldValueAsync<decimal>(3);
                    post.StartingPremiumCost = await reader.IsDBNullAsync(4) ? 0 : await reader.GetFieldValueAsync<decimal>(4);
                    post.NewPremiumCost = await reader.IsDBNullAsync(5) ? 0 : await reader.GetFieldValueAsync<decimal>(5);
                   
                    //posts.Add(post);
                };


            }
            return post;
        }

    }
    
    
}