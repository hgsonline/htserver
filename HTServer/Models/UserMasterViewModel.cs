using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
    public class UserMasterViewModel
    {

        public string Username { get; set; }
        public int UserID { get; set; }
        public string AccountId { get; set; }
        public int UserTypeID { get; set; }
        public string UserTypeName { get; set; }
        public string UserEmail { get; set; }
        public int IsFirstLogin { get; set; }
        public string DisplayName { get; set; }
    }
}
