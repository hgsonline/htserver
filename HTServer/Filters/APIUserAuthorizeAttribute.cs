using HTServer.AES256Encryption;
using HTServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using MySql.Data.MySqlClient; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HTServer.Filters
{
    public class APIUserAuthorizeAttribute : ActionFilterAttribute
    {

       HTDataContext _databasecontext;
        public APIUserAuthorizeAttribute(HTDataContext databasecontext)
        {
            _databasecontext = databasecontext;
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            StringValues authorizationToken;

            try
            {
                var encodedString = context.HttpContext.Request.Headers.TryGetValue("Token", out authorizationToken);

                if (!string.IsNullOrEmpty(authorizationToken.First()))
                {
                    var key = EncryptionLibrary.DecryptText(authorizationToken.First());

                    string[] parts = key.Split(new char[] { ':' });

                    var UserID = Convert.ToInt32(parts[0]);       // UserID
                    var RandomKey = parts[1];                     // Random Key
                    var UserTypeID = Convert.ToInt32(parts[2]);    // UserTypeID
                    long ticks = long.Parse(parts[3]);            // Ticks
                    DateTime IssuedOn = new DateTime(ticks);

                    if (UserTypeID == 2)
                    {
                        var registerModel = (from register in _databasecontext.tokenmanager
                                             where register.UserID == UserID
                                             && register.UserID == UserID
                                             select register).FirstOrDefault();


                        if (registerModel != null)
                        {
                            // Validating Time
                            var ExpiresOn = (from token in _databasecontext.tokenmanager
                                             where token.UserID == UserID
                                             select token.ExpiresOn).FirstOrDefault();

                            if ((DateTime.Now > ExpiresOn))
                            {
                                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                context.Result = new JsonResult("Unauthorized");
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            context.Result = new JsonResult("Unauthorized");
                        }
                    }
                    else
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Result = new JsonResult("Unauthorized");
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

            base.OnActionExecuting(context);
        }



        //public readonly AppDb Db;

        //public APIUserAuthorizeAttribute(AppDb db)
        //{
        //    Db = db;
        //}


        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    StringValues authorizationToken;

        //    try
        //    {
        //        var encodedString = context.HttpContext.Request.Headers.TryGetValue("Token", out authorizationToken);

        //        if (!string.IsNullOrEmpty(authorizationToken.First()))
        //        {
        //            var key = EncryptionLibrary.DecryptText(authorizationToken.First());

        //            string[] parts = key.Split(new char[] { ':' });

        //            var UserID = Convert.ToInt32(parts[0]);       // UserID
        //            var RandomKey = parts[1];                     // Random Key
        //            var UserTypeID = Convert.ToInt32(parts[2]);    // UserTypeID
        //            long ticks = long.Parse(parts[3]);            // Ticks
        //            DateTime IssuedOn = new DateTime(ticks);

        //            if (UserTypeID == 2)
        //            {
        //                List<TokenManager> Tokenlist = new List<TokenManager>();

        //                var cmd = Db.Connection.CreateCommand() as MySqlCommand;
        //                cmd.CommandText = @"SELECT * FROM `TokenManager` WHERE `UserID` = @id";
        //                cmd.Parameters.Add(new MySqlParameter
        //                {
        //                    ParameterName = "@id",
        //                    DbType = DbType.Int32,
        //                    Value = UserID,
        //                });
        //                using (var reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        Tokenlist.Add(new TokenManager()
        //                        {

        //                            TokenID = Convert.ToInt32(reader["TokenID"]),
        //                            TokenKey = reader["TokenKey"].ToString(),
        //                            IssuedOn = Convert.ToDateTime(reader["IssuedOn"]),
        //                            ExpiresOn = Convert.ToDateTime(reader["ExpiresOn"]),
        //                            CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
        //                            UserID = Convert.ToInt32(reader["UserID"])
        //                        });
        //                    }
        //                }

        //                if (Tokenlist.Count > 0)
        //                {

        //                    // Validating Time
        //                    //var ExpiresOn = (from token in _databasecontext.TokenManager
        //                    //                 where token.UserID == UserID
        //                    //                 select token.ExpiresOn).FirstOrDefault();
        //                    var ExpiresOn = DateTime.Now;
        //                    foreach (var Token in Tokenlist)
        //                    {
        //                        ExpiresOn = Token.ExpiresOn;
        //                    }

        //                    if ((DateTime.Now > ExpiresOn))
        //                    {
        //                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        //                        context.Result = new JsonResult("Unauthorized");
        //                    }
        //                    else
        //                    {

        //                    }
        //                }
        //                else
        //                {
        //                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        //                    context.Result = new JsonResult("Unauthorized");
        //                }
        //            }
        //            else
        //            {
        //                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        //                context.Result = new JsonResult("Unauthorized");
        //            }
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    base.OnActionExecuting(context);
        //}



    }
}
