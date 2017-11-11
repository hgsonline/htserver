using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HTServer.Models;
using HTServer.Filters;

namespace HTServer.Controllers
{
    [Produces("application/json")]
    [Route("api/UserDetails")]
   // [TypeFilter(typeof(APIUserAuthorizeAttribute))]
    public class UserDetailsController : Controller
    {


        HTDataContext _DatabaseContext;
        public UserDetailsController(HTDataContext DatabaseContext)
        {
            _DatabaseContext = DatabaseContext;
        }


        // POST api/values
        [HttpPost]
        public UserMasterTB Post([FromBody]CommonModel CommonModel)
        {
            try
            {
                if (string.IsNullOrEmpty(CommonModel.Username))
                {
                    return null;
                }

                var Userdetails = (from user in _DatabaseContext.usermastertb
                                   where user.Username == CommonModel.Username
                                   select user).SingleOrDefault();

                return Userdetails;
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}