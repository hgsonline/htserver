using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
    [NotMapped]
    public class ForgetpwdModel
    {
        public string Username { get; set; }
        public int UserTypeID { get; set; }
    }

}
