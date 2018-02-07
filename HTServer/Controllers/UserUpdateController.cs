using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HTServer.Models;
using MySql.Data.MySqlClient;
using System.Data;
//using Pomelo.Data.MySql;
namespace HTServer.Controllers
{
    [Produces("application/json")]
    [Route("api/UserUpdate")]
    public class UserUpdateController : Controller
    {

        HTDataContext _DatabaseContext;
        public UserUpdateController(HTDataContext DatabaseContext)
        {
            _DatabaseContext = DatabaseContext;
        }


        // POST api/values
        [HttpPost]
        public bool Post([FromBody]UserUpdateModel UserUpdateModel)
        {
            try
            {
                try
                {
                    if (string.IsNullOrEmpty(UserUpdateModel.Username))
                    {
                        return false;
                    }

                    if (string.IsNullOrEmpty(UserUpdateModel.Updatetype))
                    {
                        return false;
                    }

                    if (UserUpdateModel.Updatetype == "P")
                    {
                        if (string.IsNullOrEmpty(UserUpdateModel.Password))
                        {
                            return false;
                        }
                    }

                    using (var db = new AppDb())
                    {
                        db.Connection.Open();
                        var cmd = db.Connection.CreateCommand() as MySqlCommand;
                        cmd.CommandText = @"SELECT count(*) Cnt FROM `usermastertb` WHERE  `Username` = @id  and   `UserTypeID` = @usertypeid ";
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@id",
                            DbType = DbType.String,
                            Value = UserUpdateModel.Username,
                        });

                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@usertypeid",
                            DbType = DbType.String,
                            Value = UserUpdateModel.UserTypeID,
                        });

                        var output = 0;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                output = Convert.ToInt32(reader["Cnt"]);
                            }
                        }

                        if (output < 1)
                        {
                            return false;
                        }
                        else
                        {


                            int result = 0;

                            if (UserUpdateModel.Updatetype == "A")
                            {
                                //Set Active or deactive of users
                                var cmd1 = db.Connection.CreateCommand() as MySqlCommand;
                                
                                cmd1.CommandText = @"UPDATE usermastertb a SET a.IsActive = @isactive WHERE a.Username =  @id and a.UserTypeID = @usertypeid ; ";
                                cmd1.CommandType = CommandType.Text;
                                cmd1.Parameters.Add(new MySqlParameter
                                {
                                    ParameterName = "@id",
                                    DbType = DbType.String,
                                    Value = UserUpdateModel.Username,
                                });
                                cmd1.Parameters.Add(new MySqlParameter
                                {
                                    ParameterName = "@isactive",
                                    DbType = DbType.Int32,
                                    Value = UserUpdateModel.IsActive,
                                });
                                cmd1.Parameters.Add(new MySqlParameter
                                {
                                    ParameterName = "@usertypeid",
                                    DbType = DbType.String,
                                    Value = UserUpdateModel.UserTypeID,
                                });
                                result = cmd1.ExecuteNonQuery();

                            }
                            else if (UserUpdateModel.Updatetype == "P")
                            {
                                //Set password of the user
                                var cmd1 = db.Connection.CreateCommand() as MySqlCommand;
                                cmd1.CommandText = @"UPDATE usermastertb a SET a.Password = @password WHERE a.Username =  @id  and a.UserTypeID = @usertypeid  and a.IsActive = 1; ";
                                cmd1.CommandType = CommandType.Text;
                                cmd1.Parameters.Add(new MySqlParameter
                                {
                                    ParameterName = "@id",
                                    DbType = DbType.String,
                                    Value = UserUpdateModel.Username,
                                });
                                cmd1.Parameters.Add(new MySqlParameter
                                {
                                    ParameterName = "@password",
                                    DbType = DbType.String,
                                    Value = AES256Encryption.EncryptionLibrary.EncryptText(UserUpdateModel.Password),
                                });
                                cmd1.Parameters.Add(new MySqlParameter
                                {
                                    ParameterName = "@usertypeid",
                                    DbType = DbType.String,
                                    Value = UserUpdateModel.UserTypeID,
                                });
                                result = cmd1.ExecuteNonQuery();


                            }
                            if (result > 0)
                            { return true; }
                            else
                            { return false; }

                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}