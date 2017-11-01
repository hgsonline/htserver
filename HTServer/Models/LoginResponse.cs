using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{

    [NotMapped]
    public class LoginResponse
    {
       
        public string Username { get; set; }
        public int UserID { get; set; }
        public string Token { get; set; }
        public int UserTypeID { get; set; }
        public string AccountId { get; set; } 
        public string UserTypeName { get; set; }
        public string UserEmail { get; set; }
    }
}
