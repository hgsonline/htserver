using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
    [NotMapped]
    public class UserUpdateModel
    {
       
        public string Username { get; set; }
        public int UserTypeID { get; set; }
        //Updatetype A or P
        public string Updatetype { get; set; }
        //public enum Updatetype
        //{
        //    Active = 0,
        //    Password = 1
        //}

        public int IsActive { get; set; }
        public string Password { get; set; }

    }
}
