using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HTServer.Models;
using HTServer.Filters;
using HTServer.AES256Encryption;
using HTServer.Commonlibary;

namespace HTServer.Controllers
{
    [Produces("application/json")]
    [Route("api/ForgetPassword")]
    public class ForgetPasswordController : Controller
    {
        HTDataContext _DatabaseContext;
        private readonly IEmailService _emailService;

        public ForgetPasswordController(HTDataContext DatabaseContext, IEmailService emailService)
        {
            _DatabaseContext = DatabaseContext;
            _emailService = emailService;
        }


        // POST api/values
        [HttpPost]
        public bool Post([FromBody]ForgetpwdModel CommonModel)
        {
            try
            {
                if (string.IsNullOrEmpty(CommonModel.Username))
                {
                    return false;
                }

                 
                var Userdetails = (from user in _DatabaseContext.usermastertb
                                   where user.Username == CommonModel.Username &  user.UserTypeID == CommonModel.UserTypeID & user.IsActive == 1
                                   select user).SingleOrDefault();

                

                if (Userdetails == null )
                {

                    return false;
                }
                else
                {
                    Userdetails.Password = EncryptionLibrary.DecryptText(Userdetails.Password);

                    _emailService.SendEmailAsync("saip@hgsonline.net", "Health Trust Forget Password Request", "Welcome to Health Trust!<br/><br/>     You just send a request for forget password.<br/><br/>     <b>Your Credential Details</b><br/><br/>     User Name: " + Userdetails.Username  + " <br/>    Password: " + Userdetails.Password  + " <br/><br/>     Regards.<br/>Healt Trust Team.");

                    return true; 
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}