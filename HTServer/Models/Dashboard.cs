using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Data;
using HTServer.AES256Encryption;

namespace HTServer.Models
{
    public class Dashboard 
    {

        [Key]
        public int EmpId { get; set; }
        public Int64 TotalDivisions { get; set; }
        public Int64 TotalMembers { get; set; }
        public decimal CopayAmount { get; set; }
        public decimal StartingPremiumCost { get; set; }
        public decimal NewPremiumCost { get; set; }
       

        [JsonIgnore]
        public AppDb Db { get; set; }

        public Dashboard(AppDb db = null)
        {
            Db = db;
        }

                
    }
}
