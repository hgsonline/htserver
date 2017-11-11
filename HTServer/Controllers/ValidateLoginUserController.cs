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
        public LoginResponse Post([FromBody]UserDetails UserDetails)
        {
            try
            {
                LoginResponse loginresponse = new LoginResponse();

                if (string.IsNullOrEmpty(UserDetails.Username) || string.IsNullOrEmpty(UserDetails.Password))
                {
                    loginresponse.UserID = 0;
                    loginresponse.Username = string.Empty;
                    loginresponse.Token = string.Empty;
                    loginresponse.UserTypeID = 0;
                    loginresponse.AccountId = string.Empty;
                    loginresponse.UserTypeName = string.Empty;
                    loginresponse.UserEmail = string.Empty;
                    loginresponse.IsFirstLogin = -1;
                    loginresponse.DisplayName = string.Empty;
                    return loginresponse;
                }

                int UserTypeID = 0 ;
                
                    if (UserDetails.UserTypeName == "Admin")
                    {
                        UserTypeID = 1;
                    }
                    else if (UserDetails.UserTypeName == "Employer")
                    {
                        UserTypeID = 2;
                    }
                    else if (UserDetails.UserTypeName == "Member")
                    {
                         UserTypeID = 3;
                    }
                    else if (UserDetails.UserTypeName == "Provider")
                    {
                        UserTypeID = 4;
                    }
               
                var encryptedPassword = (from user in _DatabaseContext.usermastertb
                                         where user.Username == UserDetails.Username && user.UserTypeID == UserTypeID
                                         select user.Password).SingleOrDefault();


                if (!string.IsNullOrEmpty(encryptedPassword))
                {

                    if (EncryptionLibrary.DecryptText(encryptedPassword) == UserDetails.Password)
                    {
                        string Encryptpassword = EncryptionLibrary.EncryptText(UserDetails.Password);

                        var isUserExists = (from user in _DatabaseContext.usermastertb
                                            where user.Username == UserDetails.Username && user.Password == Encryptpassword && user.UserTypeID == UserTypeID
                                            select user).Count();

                        if (isUserExists > 0)
                        {
                            var usermastertb = (from user in _DatabaseContext.usermastertb
                                                join usertype in _DatabaseContext.usertype on user.UserTypeID equals usertype.UserTypeID
                                                where user.Username == UserDetails.Username && user.Password == Encryptpassword && user.UserTypeID == UserTypeID
                                                select new UserMasterViewModel
                                                {
                                                    UserID = user.UserID,
                                                    UserTypeID = user.UserTypeID,
                                                    UserTypeName = usertype.UserTypeName,
                                                    Username = user.Username,
                                                    AccountId = user.AccountId,
                                                    UserEmail =user.Email,
                                                    IsFirstLogin = user.IsFirstLogin, 
                                                    DisplayName = user.DisplayName
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
                                                           select temptoken).FirstOrDefault();

                                                        ////Exactly one: Single
                                                        ////One or zero: SingleOrDefault
                                                        ////One or more: First
                                                        ////Zero or more: FirstOrDefault
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
                                    loginresponse.UserID = usermastertb.UserID;
                                    loginresponse.Username = usermastertb.Username;
                                    loginresponse.Token = newToken;
                                    loginresponse.UserTypeID = usermastertb.UserTypeID;
                                    loginresponse.UserTypeName = usermastertb.UserTypeName;
                                    //if (usermastertb.UserTypeID == 1)
                                    //{
                                    //    loginresponse.UserTypeName = "Admin";
                                    //}
                                    //else if (usermastertb.UserTypeID == 2)
                                    //{
                                    //    loginresponse.UserTypeName = "Employer";
                                    //}
                                    //else if (usermastertb.UserTypeID == 3)
                                    //{
                                    //    loginresponse.UserTypeName = "Member";
                                    //}
                                    //else if (usermastertb.UserTypeID == 4)
                                    //{
                                    //    loginresponse.UserTypeName = "Provider";
                                    //}

                                    loginresponse.AccountId = usermastertb.AccountId;
                                    loginresponse.UserEmail = usermastertb.UserEmail;
                                    loginresponse.IsFirstLogin = usermastertb.IsFirstLogin;
                                    loginresponse.DisplayName = usermastertb.DisplayName;
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