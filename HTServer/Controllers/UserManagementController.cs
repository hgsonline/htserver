using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HTServer.Models;
using HTServer.Filters;
using HTServer.AES256Encryption;
using MySql.Data.MySqlClient;
using System.Data;

namespace HTServer.Controllers
{
    [Produces("application/json")]
    [Route("api/UserManagement")]
    public class UserManagementController : Controller
    {
        HTDataContext _DatabaseContext;
        public UserManagementController(HTDataContext DatabaseContext)
        {
            _DatabaseContext = DatabaseContext;
        }


        // POST api/values
        [HttpPost]
        public List<UserMasterTB> Post([FromBody]UserManagmentModel CommonModel)
        {
            try
            {
                if (string.IsNullOrEmpty(CommonModel.AccountID))
                {
                    return null;
                }
                                
                using (var db = new AppDb())
                {
                    db.Connection.Open();
                    var cmd = db.Connection.CreateCommand() as MySqlCommand;
                    if (CommonModel.UsertypeID == 3)
                    {
                        //user type member
                        cmd.CommandText = @"SELECT * FROM usermastertb where Username in ( SELECT AccountId FROM  empmemberdep where empid in (@id)) and usertypeid = 3 ";
                    }
                    else
                    {
                        //user type employer
                        cmd.CommandText = @"SELECT * FROM usermastertb where Username in ( SELECT AccountId FROM  empemployerdiv where empid in (@id)) and usertypeid = 2 ";
                    }
                    
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "@id",
                        DbType = DbType.String,
                        Value = CommonModel.AccountID,
                    });

                     
                    var Userdetails = new List<UserMasterTB>();
                   
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var post = new UserMasterTB()
                            {
                                   
                                    UserID  = Convert.ToInt32 (reader["UserID"]),
                                    Username = Convert.ToString(reader["Username"]),
                                    Password = Convert.ToString(reader["Password"]),
                                    AccountId = Convert.ToString(reader["AccountId"]),
                                    Email = Convert.ToString(reader["Email"]),
                                    UserTypeID = Convert.ToInt32(reader["UserTypeID"]),
                                    IsFirstLogin = Convert.ToInt32(reader["IsFirstLogin"]),
                                    DisplayName = Convert.ToString(reader["DisplayName"]),
                                    IsActive = Convert.ToInt32(reader["IsActive"]),
                                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])

                            };
                            Userdetails.Add(post);


                        }
                    }

                    return Userdetails;
                } 
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}