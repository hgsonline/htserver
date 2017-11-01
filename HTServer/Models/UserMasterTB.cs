using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
     
    public class UserMasterTB
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AccountId { get; set; }   
        public string Email { get; set; }
        public int UserTypeID { get; set; }
        public int IsFirstLogin { get; set; }

        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; } 
    }
}
