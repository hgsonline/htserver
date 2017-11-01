using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
    [NotMapped]
    public class UserDetails
    {
            public string Username { get; set; }
            public string Password { get; set; }
            public string AccountID { get; set; }
            public string UserTypeName { get; set; }
    }
}
