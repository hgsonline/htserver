using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HTServer.Filters;
using Microsoft.Extensions.Options;
using HTServer.Models;
using HTServer.AES256Encryption;

namespace HTServer.Controllers
{
    [Produces("application/json")]
    [Route("api/ValidateLoginUser")]
    public class ValidateLoginUserController : Controller
    {

        private readonly IOptions<MyConfigReader> config;
        HTDataContext _DatabaseContext;
        public ValidateLoginUserController(HTDataContext databasecontext, IOptions<MyConfigReader> config)
        {
            this.config = config;
            _DatabaseContext = databasecontext;
        }


        // POST api/values
        [HttpPost]
        public LoginResponse Post([FromBody]UserMasterTB UserMasterTB)
        {
            try
            {
                LoginResponse loginresponse = new LoginResponse();

                if (string.IsNullOrEmpty(UserMasterTB.Username) || string.IsNullOrEmpty(UserMasterTB.Password))
                {
                    loginresponse.Username = string.Empty;
                    loginresponse.Token = string.Empty;
                    loginresponse.UserTypeID = 0;
                    return loginresponse;
                }

                var encryptedPassword = (from user in _DatabaseContext.usermastertb
                                         where user.Username == UserMasterTB.Username
                                         select user.Password).SingleOrDefault();


                if (!string.IsNullOrEmpty(encryptedPassword))
                {

                    if (EncryptionLibrary.DecryptText(encryptedPassword) == UserMasterTB.Password)
                    {
                        string Encryptpassword = EncryptionLibrary.EncryptText(UserMasterTB.Password);

                        var isUserExists = (from user in _DatabaseContext.usermastertb
                                            where user.Username == UserMasterTB.Username && user.Password == Encryptpassword
                                            select user).Count();

                        if (isUserExists > 0)
                        {
                            var usermastertb = (from user in _DatabaseContext.usermastertb
                                                join usertype in _DatabaseContext.usertype on user.UserTypeID equals usertype.UserTypeID
                                                where user.Username == UserMasterTB.Username && user.Password == Encryptpassword
                                                select new UserMasterViewModel
                                                {
                                                    UserID = user.UserID,
                                                    UserTypeID = user.UserTypeID,
                                                    UserTypeName = usertype.UserTypeName,
                                                    Username = user.Username
                                                }).SingleOrDefault();

                            if (usermastertb != null)
                            {
                                try
                                {
                                    var isAlreadyTokenExists = (from tokenmanager in _DatabaseContext.tokenmanager
                                                                where tokenmanager.UserID == usermastertb.UserID
                                                                select tokenmanager).Count();

                                    if (isAlreadyTokenExists > 0)
                                    {
                                        var deleteToken = (from temptoken in _DatabaseContext.tokenmanager
                                                           where temptoken.UserID == usermastertb.UserID
                                                           select temptoken).SingleOrDefault();

                                        _DatabaseContext.tokenmanager.Remove(deleteToken);
                                        _DatabaseContext.SaveChanges();
                                    }

                                    var IssuedOn = DateTime.Now;
                                    var newToken = Commonlibary.KeyGenerator.GenerateToken(usermastertb);

                                    TokenManager token = new TokenManager();
                                    token.TokenID = 0;
                                    token.TokenKey = newToken;
                                    token.IssuedOn = IssuedOn;
                                    token.ExpiresOn = DateTime.Now.AddMinutes(Convert.ToInt32(this.config.Value.TokenExpiry));
                                    token.CreatedOn = DateTime.Now;
                                    token.UserID = usermastertb.UserID;
                                    var result = _DatabaseContext.tokenmanager.Add(token);

                                    _DatabaseContext.SaveChanges();
                                    loginresponse.Username = usermastertb.Username;
                                    loginresponse.Token = newToken;
                                    loginresponse.UserTypeID = usermastertb.UserTypeID;
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                            }

                            return loginresponse;
                        }
                        else
                        {
                            loginresponse.Username = string.Empty;
                            loginresponse.Token = string.Empty;
                            loginresponse.UserTypeID = 0;
                            return loginresponse;
                        }
                    }
                }

                loginresponse.Username = string.Empty;
                loginresponse.Token = string.Empty;
                loginresponse.UserTypeID = 0;
                return loginresponse;
            }
            catch (Exception)
            {

                throw;
            }

        }



        //public void GenerateToken(UserMasterViewModel usermastertb, LoginResponse loginresponse)
        //{

    //        try
    //        {
    //            var isAlreadyTokenExists = (from tokenmanager in _DatabaseContext.TokenManager
    //                                        where tokenmanager.UserID == usermastertb.UserID
    //                                        select tokenmanager).Count();

    //            if (isAlreadyTokenExists > 0)
    //            {
    //                var deleteToken = (from temptoken in _DatabaseContext.TokenManager
    //                                   where temptoken.UserID == usermastertb.UserID
    //                                   select temptoken).SingleOrDefault();

    //    _DatabaseContext.TokenManager.Remove(deleteToken);
    //                _DatabaseContext.SaveChanges();
    //            }

    //var IssuedOn = DateTime.Now;
    //var newToken = KeyGenerator.GetUniqueKey(15);

    //TokenManager token = new TokenManager();
    //token.TokenID = 0;
    //            token.TokenKey = newToken;
    //            token.IssuedOn = IssuedOn;
    //            token.ExpiresOn = DateTime.Now.AddMinutes(Convert.ToInt32(this.config.Value.TokenExpiry));
    //token.CreatedOn = DateTime.Now;
    //token.UserID = usermastertb.UserID;
    //var result = _DatabaseContext.TokenManager.Add(token);

    //_DatabaseContext.SaveChanges();
    //loginresponse.Username = usermastertb.Username;
    //loginresponse.Token = newToken;
    //loginresponse.UserTypeID = usermastertb.UserTypeID;
    //}
    //        catch (Exception)
    //        {
    //            throw;
    //        }
        //}



    }
}