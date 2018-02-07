using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
    [NotMapped]
    public class UserManagmentModel
    {
       
        public string AccountID { get; set; }
        public int UsertypeID { get; set; }
    }

}
