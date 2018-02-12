using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HTServer.Models;
using MySql.Data.MySqlClient;
//using Pomelo.Data.MySql; 
using System.Data;

namespace HTServer.Controllers
{
    [Produces("application/json")]
    [Route("api/ValidateUsername")]
    public class ValidateUsernameController : Controller
    {
        
        // POST api/values
        [HttpPost]
        public bool Post([FromBody]UserMasterTB usermastertb)
        {
            try
            {
                if (string.IsNullOrEmpty(usermastertb.Username))
                {
                    return false;
                }
                using (var db = new AppDb())
                {
                    db.Connection.Open();
                    var cmd = db.Connection.CreateCommand() as MySqlCommand;
                    cmd.CommandText = @"SELECT count(*) Cnt FROM `UserMasterTB` WHERE IsActive=1 and `Username` = @id";
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "@id",
                        DbType = DbType.String,
                        Value = usermastertb.Username,
                    });

                    var output = 0;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            output = Convert.ToInt32(reader["Cnt"]);
                        }
                    }

                    if (output > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}